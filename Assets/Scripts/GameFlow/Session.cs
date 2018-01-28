using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Represents a single running instance of a game
/// </summary>
public class Session
{
    #region Public Properties
    public ICryptoSyllable[] CurrentSyllableChoice => m_SyllableChoiceArray;
    public int MaxRounds => m_sessionParameter.RoundCount;
    /// <summary>
    /// Index of the currently active round inside this session
    /// </summary>
    public int ActiveRoundIndex { get; set; } = 0;
    public TransmissionSetup TransmissionSetup => m_TransmissionSetup;
    public byte[] LastSyllablesInput => m_TransmissionWord.syllableIndices;
    public SessionParameters SessionParams => m_sessionParameter;
    public GameResult MyGameResult { get; set; } = null;
    public TransmissionWord TransmissionWord => m_TransmissionWord;

    #endregion

    #region Constructor
    public Session(SessionParameters sessionParameter)
    {
        m_sessionParameter = sessionParameter;

        m_SyllableChoiceArray = new ICryptoSyllable[sessionParameter.SyllableChoiceAmount];
        m_SyllableSearchArray = new ICryptoSyllable[sessionParameter.SyllableSearchedAmount];

        // Create transmission flow from parameter
        SessionParameters sp = m_sessionParameter;
        m_TransmissionSetup = TransmissionManager.BuildTransmissionSetup(sp.Seed, sp.RoundCount, sp.SyllableSearchedAmount, sp.SyllableChoiceAmount);

        m_TransmissionWord = new TransmissionWord(m_TransmissionSetup.StartWord);

        Debug.Assert(m_TransmissionSetup.Transmissions.Length == sp.RoundCount, "Transmission setup creation returned tansmission array with wrong length");

        // Set the active round index to the first entry
        ActiveRoundIndex = 0;

        // Start the first round
        SetRound(ActiveRoundIndex);
    }

    public Session(SessionParameters sessionParameter, TransmissionWord transmissionWord, int currentRoundIndex)
    {
        bool validCurrentRoundIndex = currentRoundIndex >= 0 && currentRoundIndex < sessionParameter.RoundCount;
        Debug.Assert(validCurrentRoundIndex, string.Format("Tried to construct a session with invalid currentRoundIndex {0}", currentRoundIndex));
        if (!validCurrentRoundIndex)
        {
            return;
        }

        m_sessionParameter = sessionParameter;
        m_TransmissionWord = transmissionWord;

        m_SyllableChoiceArray = new ICryptoSyllable[sessionParameter.SyllableChoiceAmount];
        m_SyllableSearchArray = new ICryptoSyllable[sessionParameter.SyllableSearchedAmount];

        // Create transmission flow from parameter
        SessionParameters sp = m_sessionParameter;
        m_TransmissionSetup = TransmissionManager.BuildTransmissionSetup(sp.Seed, sp.RoundCount, sp.SyllableSearchedAmount, sp.SyllableChoiceAmount);

        // Set the active round index to the first entry
        ActiveRoundIndex = currentRoundIndex;

        // Start the first round
        SetRound(ActiveRoundIndex);
    }
    #endregion


    #region Public Methods
    public ICryptoSyllable[] GetLastInputSyllables()
    {
        ICryptoSyllable[] result = new ICryptoSyllable[m_sessionParameter.SyllableSearchedAmount];

        Debug.Assert(m_TransmissionWord.syllableIndices.Length == result.Length, "Array length of last syllable input does not match the required syllable amount");
        if (m_TransmissionWord.syllableIndices.Length != result.Length)
        {
            return result;
        }

        bool hasValidRoundIndex = ActiveRoundIndex >= 0 && ActiveRoundIndex < m_TransmissionSetup.Transmissions.Length;
        Debug.Assert(hasValidRoundIndex, "Active round index is out of range for transmission array");
        if (!hasValidRoundIndex)
        {
            return result;
        }

        Transmission currentTransmission = m_TransmissionSetup.Transmissions[ActiveRoundIndex];

        var inSyllables = currentTransmission.InLanguage.GetSyllables();

        for (int i = 0; i < m_TransmissionWord.syllableIndices.Length; ++i)
        {
            int syllableIndex = m_TransmissionWord.syllableIndices[i];
            Debug.Assert(syllableIndex >= 0 && syllableIndex < inSyllables.Length, "last syllable indices are out of range for the language excerpt");

            result[i] = inSyllables[syllableIndex];
        }

        return result;
    }

    /// <summary>
    /// Sets the a specific round in this session. Will force a restart for the given round
    /// </summary>
    public void SetRound(int index)
    {
        bool validIndex = index >= 0 && index < MaxRounds;
        Debug.Assert(validIndex, string.Format("Tried to set round with invalid index {0}", index));

        // Refill possible syllable list
        var currentTransmission = m_TransmissionSetup.Transmissions[index];
        var languageExcerpt = currentTransmission.OutLanguage;

        Debug.Assert(m_SyllableChoiceArray.Length == languageExcerpt.GetSyllables().Length, "The current language excerpt does not have a proper count of possible syllables");
        m_SyllableChoiceArray = languageExcerpt.GetSyllables();

        // Clear search list
        m_SyllableSearchArray = new ICryptoSyllable[m_sessionParameter.SyllableSearchedAmount];

        // Reset timer (is there a timer though? #gameJamGameDesign)
    }

    public void SetSyllableChoice(int index, ICryptoSyllable syllable)
    {
        bool validIndex = index >= 0 && index < m_SyllableChoiceArray.Length;
        Debug.Assert(validIndex, string.Format("Tried to set syllable at invalid index {0}", index));
        if (!validIndex)
        {
            return;
        }

        int indexInChoiceArray = -1;
        for (int i = 0; i < m_SyllableChoiceArray.Length; ++i)
        {
            var tmpSyllable = m_SyllableChoiceArray[i];
            if (tmpSyllable != syllable)
            {
                continue;
            }

            indexInChoiceArray = i;
            break;
        }
        Debug.Assert(indexInChoiceArray == -1, "Tried to set a syllable as choice, but it is not found in available syllable list");
        if (indexInChoiceArray == -1)
        {
            return;
        }

        // Remove syllable from choice array and add it to the search array
        m_SyllableChoiceArray[indexInChoiceArray] = null;

        // Check if the current search slot is replaced with a new one -> Give the option back to the user in this case
        var oldSyllable = m_SyllableSearchArray[index];
        if (oldSyllable != null)
        {
            // Find free spot where the now unused syllable can go in the choice array
            bool replacementSuccess = false;
            for (int i = 0; i < m_SyllableChoiceArray.Length; ++i)
            {
                if (m_SyllableChoiceArray[i] != null)
                {
                    continue;
                }

                // Put oldSyllable to open spot
                m_SyllableChoiceArray[i] = oldSyllable;
                replacementSuccess = true;
                break;
            }

            Debug.Assert(replacementSuccess, "Replaced a chosen syllable, but there was no space to push old syllable back to choice array");
        }

        // Update the search array with the new syllable
        m_SyllableSearchArray[index] = syllable;
    }

    public string SessionName { get; } = "Test";

    /// <summary>
    /// Returns the save game info of this game instance
    /// </summary>
    public SaveGameSession CreateSaveGame()
    {
        SaveGameSession sessionSaveGame = new SaveGameSession();

        sessionSaveGame.CurrentRound = ActiveRoundIndex;
        sessionSaveGame.SessionParameters = m_sessionParameter;
        sessionSaveGame.TransmissionWord = m_TransmissionWord;

        return sessionSaveGame;
    }
    #endregion

    #region Private Member
    private SessionParameters m_sessionParameter = null;

    /// <summary>
    /// The current list of syllable choices entered in this round
    /// </summary>
    private ICryptoSyllable[] m_SyllableChoiceArray = null;
    /// <summary>
    /// List that contains the possible syllables that the user can choose from
    /// </summary>
    private ICryptoSyllable[] m_SyllableSearchArray = null;
    /// <summary>
    /// Information about 
    /// </summary>
    private TransmissionSetup m_TransmissionSetup = null;

    /// <summary>
    /// The active transmission word
    /// </summary>
    private TransmissionWord m_TransmissionWord = null;
    #endregion
}

[System.Serializable]
public class SaveGameSession
{
    public int CurrentRound;
    public SessionParameters SessionParameters;
    public TransmissionWord TransmissionWord;
}