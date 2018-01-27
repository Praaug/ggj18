public abstract class BaseViewModel
{
    public abstract MenuEnum MenuType { get; }

    public BaseViewModel()
    {

    }

    public void Init(GameViewModel gameViewModel)
    {
        gameViewModel.OnUpdateMenu += GameViewModel_OnUpdateMenu;
    }

    private void GameViewModel_OnUpdateMenu(BaseViewModel viewModel)
    {
        if (viewModel != this)
        {
            return;
        }

        OnEnterState();
    }

    public virtual void OnEnterState() { }

    public virtual void CloseButtonCommand()
    {

    }
}
