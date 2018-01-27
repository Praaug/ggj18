using UnityEngine;

public class IncommingTransmissionView : BaseView
{
    private IncommingTransmissionViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.IncommingTransmissionViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        base.Init(m_viewModel);


    }
}
