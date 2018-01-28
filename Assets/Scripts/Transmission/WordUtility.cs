using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public static class WordUtility
{
    public static Word[] GetWordsFromFile(TextAsset textAsset)
    {
        if (!textAsset)
        {
            return null;
        }

        string fileContent = textAsset.text;
        string[] fileLines = fileContent.Split('\r');

        List<Word> wordList = new List<Word>(fileLines.Length);

        string textLine = "";
        for (int i = 0; i < fileLines.Length; ++i)
        {
            textLine = fileLines[i];

            int syllableIndex = textLine.IndexOf('=');
            if (syllableIndex < 0 || syllableIndex >= textLine.Length)
            {
                continue;
            }
            string fullSyllables = textLine.Substring(textLine.IndexOf('=') + 1);

            Word word = new Word();
            word.syllables = fullSyllables.Split('-');
            word.syllableIndices = new int[word.syllables.Length];

            wordList.Add(word);
        }

        return wordList.ToArray();
    }
}
