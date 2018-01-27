using UnityEngine;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{
    public virtual void Init(BaseViewModel viewModel)
    {
        m_viewModel = viewModel;
        m_closeButton.onClick.AddListener(OnCloseClickButton);
    }

    [SerializeField]
    private Button m_closeButton;

    private BaseViewModel m_viewModel;

    private void OnCloseClickButton()
    {
        m_viewModel.CloseButtonCommand();
    }
}
