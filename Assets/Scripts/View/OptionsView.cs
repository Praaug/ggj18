using UnityEngine;

public class OptionsView : BaseView<OptionsViewModel>
{
    private OptionsViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.OptionsViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        base.Init(m_viewModel);
    }
}
