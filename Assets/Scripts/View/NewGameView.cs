using UnityEngine;
using UnityEngine.UI;

public class NewGameView : BaseView
{
    [SerializeField]
    private Button m_startGameButton;

    private NewGameViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.NewGameViewModel;
        Debug.Assert(m_viewModel != null, "NewGameViewModel not valid");
        base.Init(m_viewModel);

        m_startGameButton.onClick.AddListener(OnStartButtonClick);
    }

    private void OnStartButtonClick()
    {
        m_viewModel.StartGameCommand();
    }
}
