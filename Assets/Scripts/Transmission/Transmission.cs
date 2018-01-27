using System.Collections.Generic;
using System.Linq;

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
    /// The syllable index conversion dictionary
    /// </summary>
    public Dictionary<byte, byte> Conversion;
    #endregion

    #region Public Methods
    /// <summary>
    /// Constructs a new transmission instance
    /// </summary>
    /// <param name="inLanguage">The incoming language</param>
    /// <param name="outLanguage">The outgoing language</param>
    /// <param name="random">The random number generator to use</param>
    public Transmission(LanguageExcerpt inLanguage, LanguageExcerpt outLanguage, System.Random random, int syllableCount)
    {
        InLanguage = inLanguage;
        OutLanguage = outLanguage;

        var tmpList = new List<byte>(syllableCount);
        Conversion = new Dictionary<byte, byte>();

        for (byte i = 0; i < syllableCount; i++)
        {
            tmpList.Add(i);
        }

        for (byte i = 0; i < syllableCount; i++)
        {
            int index = random.Next(tmpList.Count);
            Conversion.Add(i, tmpList[index]);
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
        Dictionary<byte, byte> reversion = new Dictionary<byte, byte>();
        foreach (var item in Conversion)
        {
            reversion.Add(OutLanguage.usedSyllableIndices[item.Value], InLanguage.usedSyllableIndices[item.Key]);
        }

        var outWord = new TransmissionWord();
        outWord.syllableIndices = new byte[inWord.syllableIndices.Length];

        for (int i = 0; i < inWord.syllableIndices.Length; i++)
        {
            outWord.syllableIndices[i] = reversion[inWord.syllableIndices[i]];
        }

        return outWord;
    }
    #endregion
}
