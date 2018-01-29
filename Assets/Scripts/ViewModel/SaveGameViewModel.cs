public class SaveGameViewModel : BaseViewModel
{
    public override MenuEnum MenuType => (MenuEnum)(-1);
    public SaveGame MySaveGame => m_saveGame;

    public event System.Action<SaveGameViewModel> OnLoadSaveGameCommand;

    /// <summary>
    /// The name of the Savegame
    /// </summary>
    public string Name => m_saveGame.saveGameSession.SessionParameters.SessionName;

    /// <summary>
    /// The currently played rounds
    /// </summary>
    public int CurrentRound => m_saveGame.saveGameSession.CurrentRound;

    /// <summary>
    /// The max rounds of the game
    /// </summary>
    public int MaxRounds => m_saveGame.saveGameSession.SessionParameters.RoundCount;

    public SaveGameViewModel(SaveGame saveGame)
    {
        m_saveGame = saveGame;
    }

    public void LoadSaveGameCommand()
    {
        OnLoadSaveGameCommand?.Invoke(this);
    }

    private SaveGame m_saveGame;
}
