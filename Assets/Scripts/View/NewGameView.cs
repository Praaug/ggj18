using UnityEngine;
using UnityEngine.UI;

public class NewGameView : BaseView
{
    [SerializeField]
    private Button m_startGameButton;

    private NewGameViewModel m_viewModel;

    private void Awake()
    {
        m_viewModel = new NewGameViewModel();
        Init();
    }

    private void Init()
    {
        base.Init(m_viewModel);

        m_startGameButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        m_viewModel.StartGameCommand();
    }
}
