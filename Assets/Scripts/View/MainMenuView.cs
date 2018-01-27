using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainMenuView : BaseView
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

    [SerializeField]
    private Animator m_animator;

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

        m_newGameButton.onClick.AddListener(OnNewGameButtonClick);
        m_optionsButton.onClick.AddListener(OnOptionsButtonClick);
        m_websiteButton.onClick.AddListener(OnWebsiteButtonClick);

        m_saveGameButtonViewModelList = new List<SaveGameButtonView>(m_viewModel.SaveGameViewModelList.Count);

        foreach (SaveGameViewModel sgViewModel in m_viewModel.SaveGameViewModelList)
        {
            var buttonView = GameObject.Instantiate(m_saveGameButtonPrefab, m_contentTransform) as SaveGameButtonView;
            buttonView.transform.SetAsLastSibling();
            buttonView.Init(sgViewModel);

            m_saveGameButtonViewModelList.Add(buttonView);
        }
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
