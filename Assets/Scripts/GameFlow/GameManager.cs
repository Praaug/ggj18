using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	#region Private Methods
	private void Init()
	{
		LoadSaveGames();
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
			return null;
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
		FileUtilities.CreateOrOverwriteAllText(m_saveGamePath, jsonContent);
	}
	#endregion
	#endregion

	#region Helper Methods

	#endregion

	#region Private Fields
	private GameInstance m_activeGameInstance = null;

	[SerializeField]
	private string m_saveGamePath = "";

	private List<SaveGame> m_saveGameList = new List<SaveGame>();
	#endregion
}
