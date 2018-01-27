using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainMenuView : BaseView
{
    #region Public Method
    /// <summary>
    /// Opens the Options Screen
    /// </summary>
    public void OpenOptions()
    {
        m_animator.SetBool("OptionsOpen", true);
    }

    /// <summary>
    /// Closes the Option Screen
    /// </summary>
    public void CloseOptions()
    {
        m_animator.SetBool("OptionsOpen", false);
    }

    /// <summary>
    /// Opens the newGame Screen
    /// </summary>
    public void OpenNewGame()
    {
        m_animator.SetBool("NewGameOpen", true);
    }

    /// <summary>
    /// Closes the newGame Screen
    /// </summary>
    public void CloseNewGame()
    {
        m_animator.SetBool("NewGameOpen", false);
    }

    #endregion

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
        m_viewModel = new MainMenuViewModel();
        Init(m_viewModel);
    }

    private void Init(MainMenuViewModel viewModel)
    {
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
        m_viewModel.StartNewGameCommand();
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
