using System;

public class OptionsViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.Options;

    public event Action OnCloseOptionsCommand;

    public OptionsViewModel() : base()
    {

    }

    public override void CloseButtonCommand()
    {
        OnCloseOptionsCommand?.Invoke();
    }
}
