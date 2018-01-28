using System;
/// <summary>
/// Representation of a language agnostic word
/// </summary>
[System.Serializable]
public class TransmissionWord
{
    #region Public Fields
    /// <summary>
    /// This word's syllable indices
    /// </summary>
    public byte[] syllableIndices;

    #endregion

    #region Public Methods

    public TransmissionWord()
    {
    }

    public TransmissionWord(TransmissionWord startWord)
    {
        syllableIndices = new byte[startWord.syllableIndices.Length];
        for (int i = 0; i < syllableIndices.Length; i++)
        {
            syllableIndices[i] = startWord.syllableIndices[i];
        }
    }

    public bool IsEqualTo(TransmissionWord other)
    {
        if (syllableIndices.Length != other.syllableIndices.Length)
            return false;

        for (int i = 0; i < syllableIndices.Length; i++)
        {
            if (syllableIndices[i] != other.syllableIndices[i])
                return false;
        }

        return true;
    }

    /// <summary>
    /// Represents the word in a language
    /// </summary>
    /// <param name="language">The language to set to</param>
    /// <returns>The syllable chain representation</returns>
    public ICryptoSyllable[] ToSyllables(LanguageExcerpt language)
    {
        ICryptoSyllable[] syllables = new ICryptoSyllable[syllableIndices.Length];

        var sourceSyllables = language.GetSyllables();

        for (int i = 0; i < syllables.Length; i++)
        {
            syllables[i] = sourceSyllables[syllableIndices[i]];
        }

        return syllables;
    }

    public string ToString(LanguageExcerpt sourceLanguage)
    {
        string text = string.Empty;
        var syllables = ToSyllables(sourceLanguage);
        foreach (var syllable in syllables)
        {
            text += syllable.GetSyllable().ToString();
        }

        return text;
    }
    #endregion
}
