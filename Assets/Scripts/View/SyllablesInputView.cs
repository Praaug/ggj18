using UnityEngine;

public class SyllablesInputView : BaseView
{
    private SyllablesInputViewModel m_viewModel;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        m_viewModel = GameViewModel.instance.SyllablesInputViewModel;
        Debug.Assert(m_viewModel != null, "OptionsViewModel not valid");
        m_viewModel.OnInputSyllablesChanged += ViewModel_OnInputSyllablesChanged;
        m_viewModel.OnDisplaySyllablesChanged += ViewModel_OnDisplaySyllablesChanged;
        m_viewModel.OnInTableSyllablesChanged += ViewModel_OnInTableSyllablesChanged;
        m_viewModel.OnOutTableSyllablesChanged += ViewModel_OnOutTableSyllablesChanged;
        base.Init(m_viewModel);
    }

    private void ViewModel_OnOutTableSyllablesChanged()
    {

    }

    private void ViewModel_OnInTableSyllablesChanged()
    {
        for (int i = 0; i < m_InTableSyllables.Length; i++)
        {
            if (m_InTableSyllables[i])
            {
                m_InTableSyllables[i].gameObject.SetActive(i < m_viewModel.InTableSyllables.Length);
                //m_InTableSyllables[i].s
            }
        }
    }

    private void ViewModel_OnDisplaySyllablesChanged()
    {
        throw new System.NotImplementedException();
    }

    private void ViewModel_OnInputSyllablesChanged()
    {
        throw new System.NotImplementedException();
    }

    [SerializeField]
    private UISyllable[] m_DisplaySyllables;

    [SerializeField]
    private UISyllable[] m_InputSyllables;

    [SerializeField]
    private UISyllable[] m_InTableSyllables;

    [SerializeField]
    private UISyllable[] m_OutTableSyllables;
}
