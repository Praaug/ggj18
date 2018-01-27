public abstract class BaseViewModel
{
    public abstract MenuEnum MenuType { get; }

    public BaseViewModel()
    {
        
    }

    public virtual void CloseButtonCommand()
    {

    }
}
