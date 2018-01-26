public class SaveGameViewModel
{
    /// <summary>
    /// The name of the Savegame
    /// </summary>
    public string Name => m_saveGame.Name;

    /// <summary>
    /// The currently played rounds
    /// </summary>
    public int CurrentRound => m_saveGame.CurrentRound;

    /// <summary>
    /// The max rounds of the game
    /// </summary>
    public int MaxRounds => m_saveGame.MaxRounds;

    public SaveGameViewModel(SaveGame saveGame, int index)
    {
        m_saveGame = saveGame;
        m_index = index;
    }

    public void LoadSaveGameCommand()
    {
        UnityEngine.Debug.Log("Load Save Game Commnd");
        // Todo: Model logic
    }

    private int m_index = 0;
    private SaveGame m_saveGame;
}
