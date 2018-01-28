using System;
using UnityEngine;

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

    public string RightWordString { get; private set; } = string.Empty;

    public EndScreenViewModel(GameViewModel viewModel) : base(viewModel) { }

    public void OKButtonCommand()
    {
        OnOKCommand?.Invoke();
    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        Session activeSession = GameManager.instance.ActiveSession;
        Debug.Assert(activeSession != null, "The active Session is null, this is not valid");
        GameResult result = activeSession.MyGameResult;
        IsLast = result != null;

        if (IsLast)
        {
            IsWin = result.IsWin;
            ResultString = IsWin?"YOU WON!":"YOU LOST";
            LastSessionName = result.SessionName;
            RightWordString = result.RightWord;
        }
        else
        {
            IsWin = false;
            ResultString = "send the save to the next person!";
            LastSessionName = activeSession.SessionName;
        }

        OnEnterStateAction?.Invoke();
    }
}