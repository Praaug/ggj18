using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
/// Manager class for the game's word collection
/// </summary>
[CreateAssetMenu(fileName = "WordManager", menuName = "WordManager")]
public class WordManager : ScriptableObject
{
    #region Private Fields
    /// <summary>
    /// The static word manager instance
    /// </summary>
    private static WordManager s_instance;

    /// <summary>
    /// The list of words this manager holds
    /// </summary>
    [SerializeField]
    private Word[] words;

    /// <summary>
    /// The generated human language
    /// </summary>
    private CryptoLanguageText humanLanguage;

    [System.NonSerialized]
    private bool m_isInitialized = false;
    #endregion

    #region Public Properties
    /// <summary>
    /// Singleton accessor
    /// </summary>
    public static WordManager Instance
    {
        get
        {
            if (!s_instance)
                s_instance = Resources.Load<WordManager>("Words/WordManager");
            return s_instance;
        }
    }
    #endregion

    /// <summary>
    /// Creates a transmission endpoint
    /// </summary>
    /// <param name="random">The random number generator to use</param>
    /// <returns></returns>
    public TransmissionEndpoint CreateEndpoint(int wordSyllables, int displaySyllables, System.Random random)
    {
        var wordCollection = words.Where(w => w.syllables.Length == wordSyllables).ToArray();

        var word = wordCollection[random.Next(wordCollection.Length)];

        var startWord = new TransmissionWord() { syllableIndices = word.syllableIndices };
        var humanExcerpt = new LanguageExcerpt(startWord, humanLanguage, displaySyllables, random);
        return new TransmissionEndpoint(humanExcerpt, startWord);
    }

    /// <summary>
    /// 
    /// </summary>
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (m_isInitialized)
        {
            return;
        }

#if UNITY_EDITOR
        if (!Application.isPlaying)
            return;
#endif
        BuildHumanLanguage();

        m_isInitialized = true;
    }

    private void BuildHumanLanguage()
    {
        List<string> syllableList = new List<string>();

        foreach (var word in words)
        {
            word.syllableIndices = new byte[word.syllables.Length];

            for (int i = 0; i < word.syllables.Length; i++)
            {
                var index = syllableList.IndexOf(word.syllables[i]);
                if (index == -1)
                {
                    index = syllableList.Count;
                    syllableList.Add(word.syllables[i]);
                }

                word.syllableIndices[i] = (byte)index;
            }
        }

        humanLanguage = CreateInstance<CryptoLanguageText>();
        humanLanguage.SetSyllables(syllableList);
    }

}
