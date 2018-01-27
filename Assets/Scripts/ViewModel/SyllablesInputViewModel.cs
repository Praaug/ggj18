using System;

public class SyllablesInputViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.Syllables;

    public event Action OnAcceptCommand;

    public SyllablesInputViewModel(GameViewModel gameViewModel) : base(gameViewModel)
    {

    }

    public void AcceptButtonCommand()
    {
        // open Tooltip
    }

    public void ToolTipYesCommand()
    {
        OnAcceptCommand?.Invoke();
    }

    public void ToolTipNoCommand()
    {
        // close Tooltip
    }
}