using System.Collections.Generic;

public static class TransmissionManager
{
    private static HashSet<ACryptoLanguage> languages;

    public static TransmissionSetup Setup { get; private set; }

    private static HashSet<ACryptoLanguage> LanguageSet
    {
        get
        {
            if (languages == null)
            {
                InitLanguageSets();
            }
            return languages;
        }
    }

    /// <summary>
    /// Initialises the transmission chain
    /// </summary>
    /// <param name="seed">The random seed</param>
    /// <param name="transmissionCount">The number of transmissions</param>
    /// <param name="wordLength">The number of syllables the current word has</param>
    /// <param name="displayedSyllables">The number of syllables to display as possibilities</param>
    /// <returns>The transmission setup</returns>
    public static TransmissionSetup BuildTransmissionSetup(int seed, byte transmissionCount, int wordLength, int displayedSyllables)
    {
        var random = new System.Random(seed);
        var endpoint = WordManager.Instance.CreateEndpoint(wordLength, displayedSyllables, random);

        var transmissions = new Transmission[transmissionCount];

        List<ACryptoLanguage> buffer = new List<ACryptoLanguage>(LanguageSet);

        var usedLanguages = new ACryptoLanguage[transmissionCount];

        for (int i = 0; i < transmissionCount; i++)
        {
            var index = random.Next(buffer.Count);
            usedLanguages[i] = buffer[index];
            buffer.RemoveAt(index);
            if (i > 0)
            {
                transmissions[i - 1] = new Transmission(
                    new LanguageExcerpt(usedLanguages[i], displayedSyllables, random),
                    new LanguageExcerpt(usedLanguages[i - 1], displayedSyllables, random),
                    random,
                    displayedSyllables);
            }
        }

        transmissions[transmissionCount - 1] = new Transmission(endpoint.HumanLanguage, new LanguageExcerpt(usedLanguages[transmissionCount - 1], displayedSyllables, random), random, displayedSyllables);

        var startWord = endpoint.RealWord;
        for (int i = transmissionCount - 1; i > -1; i--)
        {
            startWord = transmissions[i].Encrypt(startWord);
        }
        Setup = new TransmissionSetup(startWord, endpoint, transmissions);
        return Setup;
    }



    //public static SaveGame CreateNewSaveGame(SessionParameters sessionParameters)
    //{
    //    var seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);

    //    System.Random random = new System.Random(seed);

    //    var languageList = new List<ACryptoLanguage>(LanguageSet);

    //    var usedList = new List<ACryptoLanguage>(sessionParameters.Players);

    //    for (int i = 0; i < sessionParameters.Players && i < LanguageSet.Count; i++)
    //    {
    //        var index = random.Next(languageList.Count);
    //        usedList.Add(languageList[index]);
    //        languageList.RemoveAt(index);
    //    }

    //    return new SaveGame();
    //}

    //public static void CreateFromSaveGame(SaveGame saveGame)
    //{

    //}

    /// <summary>
    /// Loads all language sets
    /// </summary>
    private static void InitLanguageSets()
    {
        languages = new HashSet<ACryptoLanguage>(UnityEngine.Resources.LoadAll<ACryptoLanguage>("Languages"));
    }
}
