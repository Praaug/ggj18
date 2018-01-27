public class OptionsView : BaseView
{
    private OptionsViewModel m_viewModel;

    private void Awake()
    {
        m_viewModel = new OptionsViewModel();
        Init();
    }

    private void Init()
    {
        base.Init(m_viewModel);
    }
}
