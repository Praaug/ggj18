using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;


public class SaveGameRuntimeData
{
    public string filepath = "";
    public SaveGame saveGame = null;
}


[CreateAssetMenu(fileName = "GameManager", menuName = "GameManager")]
public class GameManager : ScriptableObject
{
    public GameManager()
    {
        instance = this;
    }

    #region Public Properties
    public static GameManager instance { get; private set; }

    public Session ActiveSession => m_activeSession;
    public string saveGameFolderPath => Path.Combine(Application.dataPath, m_saveGamePath);

    public GameResult GetResult()
    {
        return new GameResult();
    }

    public void ForceRandomSessionName()
    {
        StringBuilder randomName = new StringBuilder();

        // Generate random string
        for (int i = 0; i < m_randomNameGeneratorPool.Length; i++)
        {
            var possibities = m_randomNameGeneratorPool[i].possibilites;
            string subString = possibities[UnityEngine.Random.Range(0, possibities.Length)];

            randomName.Append(subString);

            if (i < m_randomNameGeneratorPool.Length - 1)
            {
                randomName.Append(' ');
            }
        }

        m_sessionParameters.SessionName = randomName.ToString();
    }

    public List<SaveGameRuntimeData> SaveGameList => m_saveGameList;
    #endregion

    #region Public Methods
    public void Init()
    {
        Debug.Assert(m_wordManager != null, "No Word manager assigned to the game manager");
        m_wordManager?.Init();

        LoadSaveGames();
    }

    public void SetParameter(SessionParameters parameter)
    {
        m_usedParameters = parameter;
    }

    public void SetDefaultParameter()
    {
        m_usedParameters = m_sessionParameters;
    }

    public SessionParameters GetParameter()
    {
        return m_usedParameters;
    }

    public void DeleteFinishedGames()
    {
        for (int i = m_saveGameList.Count - 1; i >= 0; --i)
        {
            var saveGameRuntimeData = m_saveGameList[i];
            SaveGame saveGame = saveGameRuntimeData.saveGame;
            string filepath = saveGameRuntimeData.filepath;

            if (saveGame.saveGameSession.CurrentRound < saveGame.saveGameSession.SessionParameters.RoundCount)
            {
                continue;
            }

            File.Delete(filepath);
            m_saveGameList.RemoveAt(i);
        }

        foreach (SaveGameRuntimeData saveGameRuntimeData in m_saveGameList)
        {
            SaveGame saveGame = saveGameRuntimeData.saveGame;
            string filepath = saveGameRuntimeData.filepath;

            if (saveGame.saveGameSession.CurrentRound < saveGame.saveGameSession.SessionParameters.RoundCount)
            {
                continue;
            }

            File.Delete(filepath);
        }
    }

    public void StartNewGame()
    {
        m_UsedSaveGameFilename = "";
        m_usedParameters.Seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        m_activeSession = new Session(m_usedParameters);
    }

    public void LoadGame(int index)
    {
        bool validIndex = index >= 0 && index < m_saveGameList.Count;
        Debug.Assert(validIndex, string.Format("Tried load a game with invalid index {0}", index));

        SaveGameRuntimeData saveDataRuntimeData = m_saveGameList[index];

        m_UsedSaveGameFilename = saveDataRuntimeData.filepath;
        SaveGame saveGame = saveDataRuntimeData.saveGame;

        Session newSession = new Session(saveGame.saveGameSession.SessionParameters, saveGame.saveGameSession.TransmissionWord, saveGame.saveGameSession.CurrentRound);

        m_activeSession = newSession;
    }

    public void SubmitRound()
    {
        Debug.Assert(m_activeSession != null, "Tried to submit a round, event though there is no active session");

        if (m_activeSession == null)
        {
            return;
        }

        // check if session has ended
        if (m_activeSession.MaxRounds == m_activeSession.ActiveRoundIndex + 1)
        {
            // create Game Result
            GameResult result = new GameResult();
            result.SessionName = m_activeSession.SessionName;

            TransmissionEndpoint endpoint = m_activeSession.TransmissionSetup.EndPoint;
            result.RightWord = endpoint.RealWord.ToString(endpoint.HumanLanguage);
            result.IsWin = endpoint.RealWord.IsEqualTo(m_activeSession.TransmissionWord);

            m_activeSession.MyGameResult = result;
        }

        // Increment active round index by 1
        m_activeSession.ActiveRoundIndex++;

        // save the game
        SaveCurrentGame();
    }
    #endregion

    #region Processing Methods
    public void LoadSaveGames()
    {
        // Get the list of save games from disk
        List<SaveGameRuntimeData> saveGameListOnDisk = LoadSaveGamesFromDisk();

        // Override old list with the new list from disk
        m_saveGameList = saveGameListOnDisk;
    }

    private List<SaveGameRuntimeData> LoadSaveGamesFromDisk()
    {
        string directoryPath = Path.Combine(Application.dataPath, m_saveGamePath);
        bool directoryExists = Directory.Exists(directoryPath);

        if (!directoryExists)
        {
            return new List<SaveGameRuntimeData>();
        }

        var saveGameFileList = Directory.GetFiles(directoryPath); // Array containing all save game files
        var saveGameList = new List<SaveGameRuntimeData>(saveGameFileList.Count()); //< Create list with a capacity equal to the amount of save game files

        foreach (string fileName in saveGameFileList)
        {
            string extension = Path.GetExtension(fileName);
            if (extension != ".lit")
            {
                continue;
            }

            string fileContent = File.ReadAllText(fileName);
            SaveGame saveGame = JsonUtility.FromJson<SaveGame>(fileContent);

            // Check if save game was successfully loaded
            Debug.Assert(saveGame != null, string.Format("Savegame could not be loaded from file {0}", fileName));
            if (saveGame == null)
            {
                continue;
            }

            // Add loaded savegame to the save game list
            SaveGameRuntimeData saveGameRuntimeData = new SaveGameRuntimeData();
            saveGameRuntimeData.filepath = fileName;
            saveGameRuntimeData.saveGame = saveGame;

            saveGameList.Add(saveGameRuntimeData);
        }

        // Return the list of save games
        return saveGameList;
    }

    private void SaveCurrentGame()
    {
        Debug.Assert(m_activeSession != null, "Tried to save the game, but there is no active game instance");
        if (m_activeSession == null)
        {
            return;
        }

        // Save the session to file
        SaveGame saveGame = new SaveGame();
        saveGame.saveGameSession = m_activeSession.CreateSaveGame();

        Debug.Assert(saveGame != null, "Tried to save the current game, but the save game returned by the instance was invalid");

        string jsonContent = JsonUtility.ToJson(saveGame, true);

        // Save the save game to disk
        string fileName = m_UsedSaveGameFilename;
        if (string.IsNullOrEmpty(fileName))
        {
            fileName = Path.Combine(Application.dataPath, m_saveGamePath, m_activeSession.SessionName);
            fileName = FileUtilities.GetFilepathWithTimestamp(fileName);
            fileName += ".lit";
        }

        bool fileCreationSuccess = FileUtilities.CreateOrOverwriteAllText(fileName, jsonContent);
        Debug.Assert(fileCreationSuccess, "Tried to save the current game, but file creation process failed");
    }
    #endregion

    #region Private Fields
    private Session m_activeSession = null;

    [SerializeField]
    private WordManager m_wordManager = null;
    [SerializeField]
    private string m_saveGamePath = "";

    private string m_UsedSaveGameFilename = "";

    private List<SaveGameRuntimeData> m_saveGameList = new List<SaveGameRuntimeData>();

    /// <summary>
    /// The session parameters of possible new game
    /// </summary>
    [SerializeField]
    private SessionParameters m_sessionParameters = null;

    private SessionParameters m_usedParameters = null;

    [SerializeField]
    private RandomNamePossbilities[] m_randomNameGeneratorPool = null;
    #endregion
}

[System.Serializable]
public struct RandomNamePossbilities
{
    public string[] possibilites;
}
