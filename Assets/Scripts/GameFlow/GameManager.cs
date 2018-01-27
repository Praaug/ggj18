using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "GameManager")]
public class GameManager : ScriptableObject
{
    #region Private Methods
    public void Init()
    {
        LoadSaveGames();
    }

    public void StartNewGame()
    {
        Session newSession = new Session();



        m_activeGameInstance = newSession;
    }
    #endregion


    #region Processing Methods

    #region Loading of save games
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

        Debug.Assert(directoryExists, "The directory of the save game folder does not exist");
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
    #endregion

    #region Saving of save games
    private void SaveCurrentGame()
    {
        Debug.Assert(m_activeGameInstance != null, "Tried to save the game, but there is no active game instance");
        if (m_activeGameInstance == null)
        {
            return;
        }
        SaveGame saveGame = m_activeGameInstance.CreateSaveGame();
        Debug.Assert(saveGame != null, "Tried to save the current game, but the save game returned by the instance was invalid");

        string jsonContent = JsonUtility.ToJson(saveGame);

        // Save the save game to disk
        string fileName = FileUtilities.GetFilepathWithTimestamp(m_activeGameInstance.SessionName);
        bool fileCreationSuccess = FileUtilities.CreateOrOverwriteAllText(fileName, jsonContent);
        Debug.Assert(fileCreationSuccess, "Tried to save the current game, but file creation process failed");
    }
    #endregion
    #endregion

    #region Private Fields
    private Session m_activeGameInstance = null;

    [SerializeField]
    private string m_saveGamePath = "";

    private List<SaveGame> m_saveGameList = new List<SaveGame>();
    [SerializeField]
    private UIManager m_UIManager = null;
    #endregion
}
