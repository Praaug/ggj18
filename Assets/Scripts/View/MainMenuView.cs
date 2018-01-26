using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class MainMenuView : MonoBehaviour
{
    #region Public Methods
    #endregion

    #region Private Fields
    [SerializeField]
    private Button m_closeButton;

    [SerializeField]
    private Button m_newGameButton;

    [SerializeField]
    private Button m_optionsButton;

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
        m_viewModel = new MainMenuViewModel();
        Init();
    }

    private void Init()
    {
        m_newGameButton.onClick.AddListener(OnNewGameButtonClick);
        m_closeButton.onClick.AddListener(OnCloseButtonClick);
        m_optionsButton.onClick.AddListener(OnOptionsButtonClick);

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

    private void OnCloseButtonClick()
    {
        m_viewModel.CloseApplicationCommand();
    }

    private void OnOptionsButtonClick()
    {
        m_viewModel.OpenOptionsCommand();
    }
    #endregion
}
