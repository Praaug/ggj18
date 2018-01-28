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

    [SerializeField]
    private InputField m_searchedCountInput = null;

    private new NewGameViewModel m_viewModel;

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
        m_searchedCountInput.onEndEdit.AddListener(OnSearchedCountEndEdit);
    }

    private void OnStartButtonClick()
    {
        m_viewModel.StartGameCommand();
    }

    private void OnDisplayDurationEndEdit(string inputString)
    {
        m_viewModel.ChangeDisplayDurationCommand(float.Parse(inputString));
    }

    private void OnPlayerCountEndEdit(string inputString)
    {
        int playerCount;
        if (!int.TryParse(inputString, out playerCount))
        {
            return;
        }

        m_viewModel.ChangePlayerCountCommand(playerCount);
    }

    private void OnSessionNameEndEdit(string inputString)
    {
        m_viewModel.ChangeSessionNameCommand(inputString);
    }

    private void OnSearchedCountEndEdit(string inputString)
    {
        int searchedCount;
        if (!int.TryParse(inputString, out searchedCount))
        {
            return;
        }

        m_viewModel.ChangeSearchedCountChange(searchedCount);
    }
}
