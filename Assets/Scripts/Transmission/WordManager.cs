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
	public Word[] wordList;

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
		var wordCollection = wordList.Where(w => w.syllables.Length == wordSyllables).ToArray();

		var word = wordCollection[random.Next(wordCollection.Length)];

		string msg = "Choosing word";
		for (int i = 0; i < word.syllables.Length; i++)
		{
			msg += " " + word.syllables[i];
		}

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
		LoadWords();

		BuildHumanLanguage();

		m_isInitialized = true;
	}

	private void LoadWords()
	{
		TextAsset textAsset = Resources.Load<TextAsset>("Words/Syllables");
		wordList = WordUtility.GetWordsFromFile(textAsset);
	}

	private void BuildHumanLanguage()
	{
		Dictionary<string, int> tmpMap = new Dictionary<string, int>();
		int index = 0;

		for (int i = 0; i < wordList.Length; ++i)
		{
			Word word = wordList[i];
			string[] worldSyllable = word.syllables;

			for (int j = 0; j < worldSyllable.Length; ++j)
			{
				string syllable = worldSyllable[j];

				// Check if the syllable was already used
				if (!tmpMap.ContainsKey(syllable))
				{
					// Add the new syllable and store index
					tmpMap.Add(worldSyllable[j], index);

					// Increment the index to ensure index continues to be unique
					index++;
				}

				// Get the index of the current syllable
				word.syllableIndices[j] = tmpMap[syllable];
			}
		}

		humanLanguage = CreateInstance<CryptoLanguageText>();
		humanLanguage.SetSyllables(tmpMap.Keys.ToList());
	}

}
