using UnityEngine;
using UnityEngine.UI;

public class SyllablesInputView : BaseView<SyllablesInputViewModel>
{
    private new SyllablesInputViewModel m_viewModel = null;

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        m_viewModel = GameViewModel.instance.SyllablesInputViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        m_viewModel.OnSyllablesChanged += ViewModel_OnSyllablesChanged;
        m_viewModel.OnSwitchTableCommand += ViewModel_OnSwitchTableCommand;
        m_viewModel.OnHide += ViewModel_OnHide;
        m_viewModel.OnCanTransmitChanged += ViewModel_OnCanTransmitChanged;

        m_AcceptButton.onClick.AddListener(() => m_popUp.SetActive(true));
        m_TableButton.onClick.AddListener(m_viewModel.TableButtonCommand);
        m_popUpNoButton.onClick.AddListener(() => m_popUp.SetActive(false));
        m_popUpYesButton.onClick.AddListener(m_viewModel.AcceptButtonCommand);

        for (int i = 0; i < m_InputButtons.Length; i++)
        {
            int tmp = i;
            m_InputButtons[i].onClick.AddListener(() => { m_viewModel.OnInputCommand(tmp); });
        }

        base.Init(m_viewModel);
    }

    private void ViewModel_OnCanTransmitChanged(bool canTransmit)
    {
        m_AcceptButton.interactable = canTransmit;
    }

    private void ViewModel_OnHide()
    {
        m_popUp.gameObject.SetActive(false);
        m_Table.gameObject.SetActive(false);
    }

    private void ViewModel_OnSwitchTableCommand()
    {
        Debug.LogFormat("Switching table to {0}", (m_Table.activeSelf ? "off" : "on"));
        m_Table.SetActive(!m_Table.activeSelf);
    }

    private void ViewModel_OnSyllablesChanged()
    {
        for (int i = 0; i < m_OutTableSyllables.Length; i++)
        {
            if (i < m_viewModel.OutTableSyllables.Length)
            {
                m_OutTableSyllables[i].Activate(true);
                m_OutTableSyllables[i].Init(m_viewModel.OutTableSyllables[i]);
            }
            else
            {
                m_OutTableSyllables[i].Activate(false);
            }
        }

        for (int i = 0; i < m_InTableSyllables.Length; i++)
        {
            if (i < m_viewModel.InTableSyllables.Length)
            {
                m_InTableSyllables[i].Activate(true);
                m_InTableSyllables[i].Init(m_viewModel.InTableSyllables[i]);
            }
            else
            {
                m_InTableSyllables[i].Activate(false);
            }
        }

        for (int i = 0; i < m_DisplaySyllables.Length; i++)
        {
            if (i < m_viewModel.DisplayedSyllables.Length)
            {
                m_DisplaySyllables[i].Activate(true);
                m_DisplaySyllables[i].Init(m_viewModel.DisplayedSyllables[i]);
            }
            else
            {
                m_DisplaySyllables[i].Activate(false);
            }
        }

        for (int i = 0; i < m_InputSyllables.Length; i++)
        {
            if (i < m_viewModel.InputSyllables.Length)
            {
                m_InputSyllables[i].Activate(true);
                m_InputSyllables[i].Init(m_viewModel.InputSyllables[i]);
            }
            else
            {
                m_InputSyllables[i].Activate(false);
            }
        }
    }

    [SerializeField]
    private SyllableView[] m_DisplaySyllables = null;

    [SerializeField]
    private SyllableView[] m_InputSyllables = null;

    [SerializeField]
    private SyllableView[] m_InTableSyllables = null;

    [SerializeField]
    private SyllableView[] m_OutTableSyllables = null;

    [SerializeField]
    private Button[] m_InputButtons = null;

    [SerializeField]
    private Button m_TableButton = null;

    [SerializeField]
    private Button m_AcceptButton = null;

    [SerializeField]
    private GameObject m_Table = null;

    [SerializeField]
    private GameObject m_popUp = null;

    [SerializeField]
    private Button m_popUpYesButton;

    [SerializeField]
    private Button m_popUpNoButton;
}
