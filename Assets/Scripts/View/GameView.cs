using System;
using UnityEngine;

public class GameView : BaseView<GameViewModel>
{
    [SerializeField]
    private Animator m_animator;

    private GameViewModel m_viewModel = null;

    private void Awake()
    {
        m_viewModel = GameViewModel.instance;
        Init();
    }

    private void Init()
    {
        base.Init(m_viewModel);
        m_viewModel.OnUpdateMenu += ViewModel_OnUpdateMenu;
    }

    private void ViewModel_OnUpdateMenu(BaseViewModel menu)
    {
        m_animator.SetInteger("menu", (int)menu.MenuType);
    }
}
