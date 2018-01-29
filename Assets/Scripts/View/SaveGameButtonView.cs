using System;
using UnityEngine;
using UnityEngine.UI;

public class SaveGameButtonView : MonoBehaviour
{
    public Button MyButton => m_button;

    public void Init(SaveGameViewModel viewModel)
    {
        m_button.onClick.AddListener(OnButtonClick);

        m_viewModel = viewModel;
        m_nameText.text = m_viewModel.Name;
        m_roundsText.text = string.Format("{0} / {1}", m_viewModel.CurrentRound, m_viewModel.MaxRounds);
    }

    [SerializeField]
    private Button m_button = null;

    [SerializeField]
    private Text m_nameText = null;

    [SerializeField]
    private Text m_roundsText = null;

    private SaveGameViewModel m_viewModel;

    private void OnButtonClick()
    {
        m_viewModel.LoadSaveGameCommand();
    }
}
