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

    /// <summary>
    /// Represents the word in a language
    /// </summary>
    /// <param name="language">The language to set to</param>
    /// <returns>The syllable chain representation</returns>
    public ICryptoSyllable[] ToSyllables(ACryptoLanguage language)
    {
        ICryptoSyllable[] syllables = new ICryptoSyllable[syllableIndices.Length];

        var sourceSyllables = language.GetSyllables();

        for (int i = 0; i < syllables.Length; i++)
        {
            syllables[i] = sourceSyllables[syllableIndices[i]];
        }

        return syllables;
    }

    public string ToString(ACryptoLanguage sourceLanguage)
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
