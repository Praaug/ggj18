using System;

public class NewGameViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.NewGame;

    public event Action OnStartGameCommand;
    public event Action OnCloseNewGameCommand;

    public NewGameViewModel() : base()
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
}
