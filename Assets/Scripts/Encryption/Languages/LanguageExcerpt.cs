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
    public int[] usedSyllableIndices;

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
        usedSyllableIndices = new int[displayedSyllables];

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

        var tmpList = new List<int>(); // Tmp list to store displayed indices
        for (int i = 0; i < word.syllableIndices.Length; ++i)
        {
            int index = word.syllableIndices[i]; // Get the index of the word

            // If a word contains a syllable twice, it should not be added twice
            if (tmpList.Contains(index))
            {
                continue;
            }

            // Add the index to the available list
            tmpList.Add(index);
            word.syllableIndices[i] = i;
        }

        // Fill up the list with random syllables
        for (int i = tmpList.Count; i < displayedSyllables; ++i)
        {
            int randomIndex = random.Next(SourceLanguage.syllableCount);

            // attention, might result in a very very bad and ugly, yet happy, game jam endless loop 
            while (tmpList.Contains(randomIndex))
            {
                randomIndex = (randomIndex + 1) % SourceLanguage.syllableCount;
            }

            tmpList.Add(randomIndex);
        }
        usedSyllableIndices = tmpList.ToArray();
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
