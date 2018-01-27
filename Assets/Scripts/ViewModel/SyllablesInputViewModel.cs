using System;
using System.Collections.Generic;

public class SyllablesInputViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.SyllableInput;

    public event Action OnAcceptCommand;

    public override void OnEnterState()
    {
        base.OnEnterState();

        Session session = GameManager.instance.ActiveSession;

        SessionParameters sessionParams = session.SessionParams;

        var transmission = session.TransmissionSetup.Transmissions[session.ActiveRoundIndex];

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
            m_InputSyllables[i].SetFromSyllable(outSyllables[transmission.Conversion[m_ShuffledInLanguageIndexes[i]]]);

            m_InTableSyllables[i] = new SyllableViewModel();
            m_InTableSyllables[i].SetFromSyllable(inSyllables[m_ShuffledInLanguageIndexes[i]]);
            m_OutTableSyllables[i] = new SyllableViewModel();
            m_OutTableSyllables[i].SetFromSyllable(outSyllables[transmission.Conversion[m_ShuffledInLanguageIndexes[i]]]);
        }

        for (int i = 0; i < sessionParams.SyllableChoiceAmount; i++)
        {
            m_DisplayedSyllables[i] = new SyllableViewModel();
        }
    }

    public SyllablesInputViewModel() : base()
    {

    }

    public void AcceptButtonCommand()
    {
        // open Tooltip
    }

    public void ToolTipYesCommand()
    {
        OnAcceptCommand?.Invoke();
    }

    public void ToolTipNoCommand()
    {
        // close Tooltip
    }

    private SyllableViewModel[] m_DisplayedSyllables;

    private SyllableViewModel[] m_InputSyllables;

    private SyllableViewModel[] m_InTableSyllables;

    private SyllableViewModel[] m_OutTableSyllables;

    private byte[] m_ShuffledInLanguageIndexes;
}