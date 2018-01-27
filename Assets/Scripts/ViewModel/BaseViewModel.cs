public abstract class BaseViewModel
{
    public abstract MenuEnum MenuType { get; }

    /// <summary>
    /// The ref index of the view model
    /// </summary>
    public int Index { get; set; } = -1;

    public BaseViewModel()
    {

    }
    public BaseViewModel(GameViewModel gameViewModel)
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
