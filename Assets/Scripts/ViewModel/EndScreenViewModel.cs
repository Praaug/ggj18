using System;

public class EndScreenViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.EndScreen;

    public event Action OnOKCommand;

    public event Action OnEnterStateAction;

    public bool IsWin { get; private set; } = false;

    public bool IsLast { get; private set; } = false;

    public string LastSessionName { get; private set; } = string.Empty;

    public string ResultString { get; private set; } = string.Empty;

    public string DescriptionString { get; private set; } = string.Empty;

    public EndScreenViewModel() : base()
    {

    }

    public void OKButtonCommand()
    {
        OnOKCommand?.Invoke();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        GameResult result = GameManager.instance.ActiveSession?.MyGameResult;
        IsLast = result != null;

        if(IsLast)
        {
            IsWin = result.IsWin;
            ResultString = "YOU WON!";
            LastSessionName = result.SessionName;
        }
        else
        {
            IsWin = false;
            ResultString = "send the save to the next person!";
            LastSessionName = result.SessionName;
        }

        OnEnterStateAction?.Invoke();
    }
}