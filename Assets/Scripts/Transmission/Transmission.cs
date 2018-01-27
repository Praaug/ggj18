using System.Collections.Generic;

/// <summary>
/// Representation of a transmission between two languages
/// </summary>
public class Transmission
{
    #region Public Fields
    /// <summary>
    /// The incoming language
    /// </summary>
    public LanguageExcerpt InLanguage;

    /// <summary>
    /// The outgoing language
    /// </summary>
    public LanguageExcerpt OutLanguage;

    /// <summary>
    /// The syllable index conversion array
    /// </summary>
    public byte[] Conversion;
    #endregion

    #region Public Methods
    /// <summary>
    /// Constructs a new transmission instance
    /// </summary>
    /// <param name="inLanguage">The incoming language</param>
    /// <param name="outLanguage">The outgoing language</param>
    /// <param name="random">The random number generator to use</param>
    public Transmission(LanguageExcerpt inLanguage, LanguageExcerpt outLanguage, System.Random random)
    {
        int syllableCount = 8;

        InLanguage = inLanguage;
        OutLanguage = outLanguage;

        var tmpList = new List<byte>(syllableCount);

        for (byte i = 0; i < syllableCount; i++)
        {
            tmpList[i] = i;
        }

        for (byte i = 0; i < syllableCount; i++)
        {
            int index = random.Next(tmpList.Count);
            Conversion[i] = tmpList[index];
            tmpList.RemoveAt(index);
        }
    }

    /// <summary>
    /// Encrypts a word by converting it from outgoing language to incoming language
    /// </summary>
    /// <param name="inWord">The word to translate</param>
    /// <returns></returns>
    public TransmissionWord Encrypt(TransmissionWord inWord)
    {
        byte[] reversion = new byte[Conversion.Length];
        for (byte i = 0; i < reversion.Length; i++)
        {
            reversion[Conversion[i]] = i;
        }

        var outWord = new TransmissionWord();
        outWord.syllableIndices = new byte[inWord.syllableIndices.Length];

        for (byte i = 0; i < inWord.syllableIndices.Length; i++)
        {
            outWord.syllableIndices[i] = reversion[inWord.syllableIndices[i]];
        }

        return outWord;
    }
    #endregion
}
