using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

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

    public GameResult GetResult()
    {
        return new GameResult();
    }

    public List<SaveGame> SaveGameList => m_saveGameList;
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
        m_sessionParameters = parameter;
    }

    public SessionParameters GetParameter()
    {
        return m_sessionParameters;
    }

    public void StartNewGame()
    {
        m_sessionParameters.Seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        m_activeSession = new Session(m_sessionParameters);
    }

    public void LoadGame(int index)
    {
        bool validIndex = index >= 0 && index < m_saveGameList.Count;
        Debug.Assert(validIndex, string.Format("Tried load a game with invalid index {0}", index));

        SaveGame saveGame = m_saveGameList[index];

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
            result.IsWin = result.RightWord == m_activeSession.LastSyllablesInput.ToString();

            m_activeSession.MyGameResult = result;
        }

        SaveCurrentGame();
    }
    #endregion

    #region Processing Methods
    private void LoadSaveGames()
    {
        // Get the list of save games from disk
        List<SaveGame> saveGameListOnDisk = LoadSaveGamesFromDisk();

        // Override old list with the new list from disk
        m_saveGameList = saveGameListOnDisk;
    }

    private List<SaveGame> LoadSaveGamesFromDisk()
    {
        string directoryPath = Path.GetDirectoryName(m_saveGamePath);
        bool directoryExists = Directory.Exists(directoryPath);

        if (!directoryExists)
        {
            return new List<SaveGame>();
        }

        string[] saveGameFileArray = Directory.GetFiles(directoryPath); // Array containing all save game files
        List<SaveGame> saveGameList = new List<SaveGame>(saveGameFileArray.Length); //< Create list with a capacity equal to the amount of save game files

        foreach (string fileName in Directory.GetFiles(directoryPath))
        {
            string fileContent = File.ReadAllText(fileName);
            var saveGame = JsonUtility.FromJson<SaveGame>(fileContent);

            // Check if save game was successfully loaded
            Debug.Assert(saveGame != null, string.Format("Savegame could not be loaded from file {0}", fileName));
            if (saveGame == null)
            {
                continue;
            }

            // Add loaded savegame to the save game list
            saveGameList.Add(saveGame);
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

        string jsonContent = JsonUtility.ToJson(saveGame);

        // Save the save game to disk
        string fileName = FileUtilities.GetFilepathWithTimestamp(m_activeSession.SessionName);
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

    private List<SaveGame> m_saveGameList = new List<SaveGame>();

    /// <summary>
    /// The session parameters of possible new game
    /// </summary>
    [SerializeField]
    private SessionParameters m_sessionParameters = null;
    #endregion
}
