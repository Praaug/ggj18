using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Representation of the part of the language displayed in the current session
/// </summary>
public class LanguageExcerpt
{
    #region Public Fields
    /// <summary>
    /// The syllable indices this language excerpt uses
    /// </summary>
    public byte[] usedSyllableIndices;

    /// <summary>
    /// This excerpt's source language
    /// </summary>
    public ACryptoLanguage SourceLanguage;
    #endregion

    #region Public Properties
    /// <summary>
    /// Returns the language type
    /// </summary>
    public LanguageType Type { get { return SourceLanguage.Type; } }
    #endregion

    #region Public Methods

    /// <summary>
    /// Construct a new language excerpt
    /// </summary>
    /// <param name="sourceLanguage">The source language to draw from</param>
    /// <param name="displayedSyllables">The number of displayed syllables</param>
    /// <param name="random">The random number generator to use</param>
    public LanguageExcerpt(ACryptoLanguage sourceLanguage, int displayedSyllables, System.Random random)
    {
        SourceLanguage = sourceLanguage;
        usedSyllableIndices = new byte[displayedSyllables];

        List<byte> indexList = new List<byte>(SourceLanguage.syllableCount);

        for (byte i = 0; i < SourceLanguage.syllableCount; i++)
        {
            indexList.Add(i);
        }

        for (int i = 0; i < displayedSyllables; i++)
        {
            var index = random.Next(indexList.Count);
            usedSyllableIndices[i] = indexList[index];
            indexList.RemoveAt(index);
        }
    }

    /// <summary>
    /// Construct a new language excerpt
    /// </summary>
    /// <param name="word"></param>
    /// <param name="sourceLanguage">The source language to draw from</param>
    /// <param name="displayedSyllables">The number of displayed syllables</param>
    /// <param name="random">The random number generator to use</param>
    public LanguageExcerpt(TransmissionWord word, ACryptoLanguage sourceLanguage, int displayedSyllables, System.Random random)
    {
        SourceLanguage = sourceLanguage;
        usedSyllableIndices = new byte[displayedSyllables];
        for (int i = 0; i < displayedSyllables; i++)
        {
            usedSyllableIndices[i] = 255;
        }

        List<byte> indexList = new List<byte>(SourceLanguage.syllableCount - word.syllableIndices.Length);

        for (byte i = 0; i < SourceLanguage.syllableCount; i++)
        {
            if (!word.syllableIndices.Contains(i))
                indexList.Add(i);
        }

        var syllableIndex = 0;
        for (int i = 0; i < word.syllableIndices.Length; i++)
        {
            if (!usedSyllableIndices.Contains(word.syllableIndices[i]))
            {
                usedSyllableIndices[syllableIndex] = word.syllableIndices[i];
                syllableIndex++;
            }
        }

        for (int i = syllableIndex; i < displayedSyllables; i++)
        {
            var index = random.Next(indexList.Count);
            usedSyllableIndices[i] = indexList[index];
            indexList.RemoveAt(index);
        }

        for (int i = 0; i < word.syllableIndices.Length; i++)
        {
            for (byte j = 0; j < usedSyllableIndices.Length; j++)
            {
                if (word.syllableIndices[i] == usedSyllableIndices[j])
                {
                    word.syllableIndices[i] = j;
                    break;
                }
            }
        }
    }

    public ICryptoSyllable[] GetSyllables()
    {
        ICryptoSyllable[] syllables = new ICryptoSyllable[usedSyllableIndices.Length];

        var sourceSyllables = SourceLanguage.GetSyllables();

        for (int i = 0; i < syllables.Length; i++)
        {
            syllables[i] = sourceSyllables[usedSyllableIndices[i]];
        }

        return syllables;
    }
    #endregion
}
