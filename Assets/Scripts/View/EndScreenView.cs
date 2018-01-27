using UnityEngine;
using UnityEngine.UI;

public class EndScreenView : BaseView
{
    [SerializeField]
    private int m_normalTextSize = 80;

    [SerializeField]
    private int m_lastTextSize = 160;

    [SerializeField]
    private float m_normalShadowSize = 3.0f;

    [SerializeField]
    private float m_resultShadowSize = 5.0f;

    [SerializeField]
    private Color m_normalTextColor = Color.white;

    [SerializeField]
    private Color m_normalShadowColor = Color.white;

    [SerializeField]
    private Color m_winTextColor = Color.green;

    [SerializeField]
    private Color m_winShadowColor = Color.green;

    [SerializeField]
    private Color m_looseTextColor = Color.red;

    [SerializeField]
    private Color m_looseShadowColor = Color.red;

    [SerializeField]
    private Text m_resultText;

    [SerializeField]
    private Text m_sessionNameText;

    [SerializeField]
    private Shadow m_resultShadow;

    private EndScreenViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.EndScreenViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        base.Init(m_viewModel);
    }
}
