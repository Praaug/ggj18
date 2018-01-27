using System;

public class NewGameViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.NewGame;

    public event Action OnStartGameCommand;
    public event Action OnCloseNewGameCommand;
    public event Action<float> OnDisplayDurationChange;
    public event Action<int> OnPlayerCountChange;
    public event Action<string> OnSessionNameChange;

    public NewGameViewModel(GameViewModel gameViewModel) : base(gameViewModel)
    {

    }

    public override void CloseButtonCommand()
    {
        OnCloseNewGameCommand?.Invoke();
    }

    public void StartGameCommand()
    {
        OnStartGameCommand?.Invoke();
    }

    public void ChangeDisplayDurationCommand(float duration)
    {
        OnDisplayDurationChange?.Invoke(duration);
    }

    public void ChangePlayerCountCommand(int playerCount)
    {
        OnPlayerCountChange?.Invoke(playerCount);
    }

    public void ChangeSessionNameCommand(string sessionName)
    {
        OnSessionNameChange?.Invoke(sessionName);
    }
}
