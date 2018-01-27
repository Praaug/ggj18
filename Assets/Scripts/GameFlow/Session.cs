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
    public int MaxRounds => m_maxRounds;
    public int ActiveRoundIndex => m_activeRoundIndex;
    public TransmissionSetup TransmissionSetup => m_TransmissionSetup;
    public byte[] LastSyllablesInput => m_LastSyllableInput;
    #endregion

    #region Constructor
    public Session(SessionParameters sessionParameter)
    {
        m_sessionParameter = sessionParameter;

        // Create transmission flow from parameter
        CreateTransmissionSetup();

        // Excerpt the possible syllable list

        // Reset 
        m_maxRounds = m_sessionParameter.SyllableSearchedAmount;

        // Set the active round index to the first entry
        m_activeRoundIndex = 0;

        // Start the first round
        SetRound(m_activeRoundIndex);
    }
    #endregion


    #region Public Methods
    public ICryptoSyllable[] GetLastInputSyllables()
    {
        ICryptoSyllable[] result = new ICryptoSyllable[m_sessionParameter.SyllableSearchedAmount];

        Debug.Assert(m_LastSyllableInput.Length == result.Length, "Array length of last syllable input does not match the required syllable amount");
        if (m_LastSyllableInput.Length != result.Length)
        {
            return result;
        }

        bool hasValidRoundIndex = m_activeRoundIndex >= 0 && m_activeRoundIndex < m_TransmissionSetup.Transmissions.Length;
        Debug.Assert(hasValidRoundIndex, "Active round index is out of range for transmission array");
        if (!hasValidRoundIndex)
        {
            return result;
        }

        Transmission currentTransmission = m_TransmissionSetup.Transmissions[m_activeRoundIndex];

        var inSyllables = currentTransmission.InLanguage.GetSyllables();

        for (int i = 0; i < m_LastSyllableInput.Length; ++i)
        {
            int syllableIndex = m_LastSyllableInput[i];
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
        bool validIndex = index >= 0 && index < m_maxRounds;
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

        sessionSaveGame.CurrentRound = m_activeRoundIndex;
        sessionSaveGame.MaxRounds = m_maxRounds;
        sessionSaveGame.SessionParameters = m_sessionParameter;

        return sessionSaveGame;
    }
    #endregion

    #region Private Methods
    private void CreateTransmissionSetup()
    {
        SessionParameters sp = m_sessionParameter;
        m_TransmissionSetup = TransmissionManager.BuildTransmissionSetup(sp.Seed, sp.RoundCount, sp.SyllableSearchedAmount, sp.SyllableChoiceAmount);

        Debug.Assert(m_TransmissionSetup.Transmissions.Length == sp.RoundCount, "Transmission setup creation returned tansmission array with wrong length");
    }
    #endregion

    #region Private Member
    private SessionParameters m_sessionParameter = null;

    /// <summary>
    /// Index of the currently active round inside this session
    /// </summary>
    private int m_activeRoundIndex = 0;

    /// <summary>
    /// The amount of rounds before the session will finish
    /// </summary>
    private int m_maxRounds = 0;


    /// <summary>
    ///  Array containing the indices of the last used syllables
    /// </summary>
    private byte[] m_LastSyllableInput = null;

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
    #endregion
}

public class SaveGameSession
{
    public int CurrentRound;
    public int MaxRounds;
    public SessionParameters SessionParameters;
}