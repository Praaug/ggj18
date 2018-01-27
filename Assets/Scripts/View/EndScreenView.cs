using UnityEngine;
using UnityEngine.UI;

public class EndScreenView : BaseView<EndScreenViewModel>
{
    [SerializeField]
    private int m_normalTextSize = 80;

    [SerializeField]
    private int m_resultTextSize = 160;

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

    private EndScreenViewModel m_viewModel = null;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.EndScreenViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        base.Init(m_viewModel);
        m_viewModel.OnEnterStateAction += ViewModel_OnEnterStateAction;
    }

    private void ViewModel_OnEnterStateAction()
    {
        m_resultText.text = m_viewModel.ResultString;
        m_sessionNameText.text = m_viewModel.LastSessionName;
        Vector2 effectDistance = Vector2.zero;

        if (m_viewModel.IsLast)
        {
            m_resultText.fontSize = m_resultTextSize;
            effectDistance.x = m_resultShadowSize;
            effectDistance.y = -m_resultShadowSize;
            m_resultShadow.effectDistance = effectDistance;

            if (m_viewModel.IsWin)
            {
                m_resultText.color = m_winTextColor;
                m_resultShadow.effectColor = m_winShadowColor;
            }
            else
            {
                m_resultText.color = m_looseTextColor;
                m_resultShadow.effectColor = m_looseShadowColor;
            }
        }
        else
        {
            m_resultText.color = m_normalTextColor;
            m_resultText.fontSize = m_normalTextSize;
            m_resultShadow.effectColor = m_normalShadowColor;
            effectDistance.x = m_normalShadowSize;
            effectDistance.y = -m_normalShadowSize;
            m_resultShadow.effectDistance = effectDistance;
        }
    }
}
