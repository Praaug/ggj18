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
    private Button m_openSavegamebutton;

    [SerializeField]
    private SaveGameButtonView m_saveGameButtonPrefab;

    [SerializeField]
    private RectTransform m_contentTransform;

    private MainMenuViewModel m_viewModel;

    private List<SaveGameButtonView> m_saveGameButtonViewModelList;
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

        m_newGameButton.onClick.AddListener(m_viewModel.OpenNewGameCommand);
        m_optionsButton.onClick.AddListener(m_viewModel.OpenOptionsCommand);
        m_websiteButton.onClick.AddListener(m_viewModel.OpenWebsiteCommand);
        m_openSavegamebutton.onClick.AddListener(m_viewModel.OpenSavegameFolderCommand);

        m_saveGameButtonViewModelList = new List<SaveGameButtonView>(m_viewModel.SaveGameViewModelList.Count);

        foreach (SaveGameViewModel sgViewModel in m_viewModel.SaveGameViewModelList)
        {
            var buttonView = Instantiate(m_saveGameButtonPrefab, m_contentTransform) as SaveGameButtonView;
            buttonView.transform.SetAsLastSibling();
            buttonView.Init(sgViewModel);

            m_saveGameButtonViewModelList.Add(buttonView);
        }
    }
    #endregion
}
