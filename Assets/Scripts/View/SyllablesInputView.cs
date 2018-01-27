using UnityEngine;

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
    private SyllableView[] m_DisplaySyllables = null;

    [SerializeField]
    private SyllableView[] m_InputSyllables = null;

    [SerializeField]
    private SyllableView[] m_InTableSyllables = null;

    [SerializeField]
    private SyllableView[] m_OutTableSyllables = null;
}
