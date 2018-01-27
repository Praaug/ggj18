using UnityEngine;

public class SyllablesInputView : BaseView
{
    private SyllablesInputViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.SyllablesInputViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        base.Init(m_viewModel);
    }
}
