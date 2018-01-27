using UnityEngine;
using UnityEngine.UI;

public abstract class BaseView<T> : MonoBehaviour where T : BaseViewModel
{
    protected T ViewModelConcrete => m_viewModel as T;

    public virtual void Init(BaseViewModel viewModel)
    {
        m_viewModel = viewModel;
        if (m_closeButton != null)
        {
            m_closeButton.onClick.AddListener(OnCloseClickButton);
        }
    }

    [SerializeField]
    private Button m_closeButton;

    protected BaseViewModel m_viewModel;

    private void OnCloseClickButton()
    {
        m_viewModel.CloseButtonCommand();
    }
}
