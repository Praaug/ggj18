using UnityEngine;

public class SyllablesView : BaseView
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
