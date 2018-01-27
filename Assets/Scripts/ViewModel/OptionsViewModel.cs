using System;

public class OptionsViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.Options;

    public event Action OnCloseOptionsCommand;

    public OptionsViewModel(GameViewModel gameViewModel) : base(gameViewModel)
    {

    }

    public override void CloseButtonCommand()
    {
        OnCloseOptionsCommand?.Invoke();
    }
}
