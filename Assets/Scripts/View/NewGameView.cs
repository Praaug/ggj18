using System;
using UnityEngine;
using UnityEngine.UI;

public class NewGameView : BaseView<NewGameViewModel>
{

    [SerializeField]
    private Button m_startGameButton = null;

    [SerializeField]
    private InputField m_displayDurationInput = null;

    [SerializeField]
    private InputField m_playerCountInput = null;

    [SerializeField]
    private InputField m_sessionNameInput = null;

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
        m_displayDurationInput.onEndEdit.AddListener(OnDisplayDurationEndEdit);
        m_playerCountInput.onEndEdit.AddListener(OnPlayerCountEndEdit);
        m_sessionNameInput.onEndEdit.AddListener(OnSessionNameEndEdit);
    }

    private void OnSessionNameEndEdit(string inputString)
    {

    }

    private void OnStartButtonClick()
    {
        m_viewModel.StartGameCommand();
    }

    private void OnDisplayDurationEndEdit(string inputString)
    {
        m_viewModel.ChangeDisplayDurationCommand(int.Parse(inputString));
    }

    private void OnPlayerCountEndEdit(string inputString)
    {
        m_viewModel.ChangePlayerCountCommand(int.Parse(inputString));
    }

}
