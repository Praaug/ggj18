using System;

class EndScreenViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.EndScreen;

    public event Action OnOKCommand;

    public EndScreenViewModel() : base()
    {

    }

    public void OKButtonCommand()
    {
        OnOKCommand?.Invoke();
    }
}