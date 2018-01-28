using System;
using System.Collections.Generic;

public class SyllablesInputViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.SyllableInput;

    public SyllableViewModel[] InputSyllables => m_InputSyllables;

    public SyllableViewModel[] InTableSyllables => m_InTableSyllables;

    public SyllableViewModel[] OutTableSyllables => m_OutTableSyllables;

    public SyllableViewModel[] DisplayedSyllables => m_DisplayedSyllables;

    public event Action OnAcceptCommand;

    public event Action OnSwitchTableCommand;

    public event Action OnSyllablesChanged;

    public SyllablesInputViewModel(GameViewModel gameViewModel) : base(gameViewModel) { }

    public override void OnEnterState()
    {
        base.OnEnterState();
        m_InputIndex = 0;
        m_Session = GameManager.instance.ActiveSession;

        SessionParameters sessionParams = m_Session.SessionParams;

        var transmission = m_Session.TransmissionSetup.Transmissions[m_Session.ActiveRoundIndex];

        // Resize array
        m_DisplayedSyllables = new SyllableViewModel[sessionParams.SyllableSearchedAmount];

        m_InputSyllables = new SyllableViewModel[sessionParams.SyllableChoiceAmount];

        m_InTableSyllables = new SyllableViewModel[sessionParams.SyllableChoiceAmount];

        m_OutTableSyllables = new SyllableViewModel[sessionParams.SyllableChoiceAmount];



        m_ShuffledInLanguageIndexes = new byte[sessionParams.SyllableChoiceAmount];

        var tmpList = new List<byte>(sessionParams.SyllableChoiceAmount);
        for (byte i = 0; i < sessionParams.SyllableChoiceAmount; i++)
        {
            tmpList.Add(i);
        }

        for (int i = 0; i < sessionParams.SyllableChoiceAmount; i++)
        {
            var index = UnityEngine.Random.Range(0, tmpList.Count);
            m_ShuffledInLanguageIndexes[i] = tmpList[index];
            tmpList.RemoveAt(index);
        }

        var inSyllables = transmission.InLanguage.GetSyllables();
        var outSyllables = transmission.OutLanguage.GetSyllables();

        for (int i = 0; i < sessionParams.SyllableChoiceAmount; i++)
        {
            m_InputSyllables[i] = new SyllableViewModel();
            m_InTableSyllables[i] = new SyllableViewModel();
            m_OutTableSyllables[i] = new SyllableViewModel();
        }

        for (int i = 0; i < sessionParams.SyllableSearchedAmount; i++)
        {
            m_DisplayedSyllables[i] = new SyllableViewModel();
        }

        OnSyllablesChanged?.Invoke();

        for (int i = 0; i < sessionParams.SyllableChoiceAmount; i++)
        {
            m_InputSyllables[i].SetFromSyllable(outSyllables[transmission.Conversion[m_ShuffledInLanguageIndexes[i]]]);
            m_InTableSyllables[i].SetFromSyllable(inSyllables[m_ShuffledInLanguageIndexes[i]]);
            m_OutTableSyllables[i].SetFromSyllable(outSyllables[transmission.Conversion[m_ShuffledInLanguageIndexes[i]]]);
        }
    }

    public void OnInputCommand(int inputIndex)
    {
        var transmission = m_Session.TransmissionSetup.Transmissions[m_Session.ActiveRoundIndex];

        UnityEngine.Debug.LogFormat("Pressed: {0}, setting: {1}", inputIndex, transmission.Conversion[m_ShuffledInLanguageIndexes[inputIndex]]);

        var syllableIndex = transmission.Conversion[m_ShuffledInLanguageIndexes[inputIndex]];
        m_Session.TransmissionWord.syllableIndices[m_InputIndex] = syllableIndex;

        var lang = m_Session.TransmissionSetup.Transmissions[m_Session.ActiveRoundIndex].OutLanguage;
        m_DisplayedSyllables[m_InputIndex].SetFromSyllable(lang.GetSyllables()[syllableIndex]);

        m_InputIndex = (m_InputIndex + 1) % m_Session.SessionParams.SyllableSearchedAmount;
    }

    public SyllablesInputViewModel() : base()
    {

    }

    public void AcceptButtonCommand()
    {
        // TODO: open Tooltip

        ToolTipYesCommand();
    }

    public void ToolTipYesCommand()
    {
        OnAcceptCommand?.Invoke();
    }

    public void ToolTipNoCommand()
    {
        // close Tooltip
    }

    public void TableButtonCommand()
    {
        UnityEngine.Debug.Log("Table switch!");
        OnSwitchTableCommand?.Invoke();
    }

    private void Awake()
    {

    }

    private SyllableViewModel[] m_DisplayedSyllables;

    private SyllableViewModel[] m_InputSyllables;

    private SyllableViewModel[] m_InTableSyllables;

    private SyllableViewModel[] m_OutTableSyllables;

    private byte[] m_ShuffledInLanguageIndexes;

    private Session m_Session;

    private int m_InputIndex;
}