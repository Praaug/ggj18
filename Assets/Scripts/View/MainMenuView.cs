using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainMenuView : BaseView<MainMenuViewModel>
{
    #region Private Fields
    [SerializeField]
    private Button m_newGameButton;

    [SerializeField]
    private Button m_optionsButton;

    [SerializeField]
    private Button m_websiteButton;

    [SerializeField]
    private SaveGameButtonView m_saveGameButtonPrefab;

    [SerializeField]
    private RectTransform m_contentTransform;

    private MainMenuViewModel m_viewModel;

    private List<SaveGameButtonView> m_saveGameButtonViewModelList = new List<SaveGameButtonView>();
    #endregion

    #region Private Methods
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.MainMenuViewModel;
        Debug.Assert(m_viewModel != null, "MainMenuViewModel not valid");

        base.Init(m_viewModel);

        ViewModelConcrete.OnUpdateSaveGameList += ViewModelConcrete_OnUpdateSaveGameList;

        m_newGameButton.onClick.AddListener(OnNewGameButtonClick);
        m_optionsButton.onClick.AddListener(OnOptionsButtonClick);
        m_websiteButton.onClick.AddListener(OnWebsiteButtonClick);

        UpdateView();
    }

    private void UpdateView()
    {
        for (int i = m_saveGameButtonViewModelList.Count - 1; i >= 0; --i)
        {
            Destroy(m_saveGameButtonViewModelList[i].gameObject);
        }

        m_saveGameButtonViewModelList = new List<SaveGameButtonView>(m_viewModel.SaveGameViewModelList.Count);

        foreach (SaveGameViewModel sgViewModel in m_viewModel.SaveGameViewModelList)
        {
            var buttonView = Instantiate(m_saveGameButtonPrefab, m_contentTransform) as SaveGameButtonView;
            buttonView.transform.SetAsLastSibling();
            buttonView.Init(sgViewModel);

            SaveGameSession saveGame = sgViewModel.MySaveGame.saveGameSession;
            buttonView.MyButton.interactable = saveGame.CurrentRound < saveGame.SessionParameters.RoundCount;

            m_saveGameButtonViewModelList.Add(buttonView);
        }

        foreach (var saveGameButton in m_saveGameButtonViewModelList)
        {
            if (saveGameButton.MyButton.IsInteractable())
            {
                continue;
            }

            saveGameButton.transform.SetAsLastSibling();
        }
    }


    private void ViewModelConcrete_OnUpdateSaveGameList()
    {
        UpdateView();
    }

    private void OnNewGameButtonClick()
    {
        m_viewModel.OpenNewGameCommand();
    }

    private void OnOptionsButtonClick()
    {
        m_viewModel.OpenOptionsCommand();
    }

    private void OnWebsiteButtonClick()
    {
        m_viewModel.OpenWebsiteCommand();
    }
    #endregion
}
