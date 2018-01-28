public abstract class BaseViewModel
{
    public event System.Action OnShow;
    public event System.Action OnHide;

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
            // Hide this view model
            Hide();
            return;
        }

        Show(); // Show this viewmodel
        OnEnterState();
    }

    private void Show()
    {
        OnShow?.Invoke();
    }

    private void Hide()
    {
        OnHide?.Invoke();
    }

    public virtual void OnEnterState() { }

    public virtual void CloseButtonCommand()
    {

    }
}
