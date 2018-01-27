using System;

class SyllablesViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.Syllables;

    public event Action OnAcceptCommand;

    public SyllablesViewModel() : base()
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