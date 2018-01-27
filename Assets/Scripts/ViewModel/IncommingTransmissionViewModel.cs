using System;
using UnityEngine;

public class IncommingTransmissionViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.IncommingTransmission;

    public IncommingTransmissionViewModel() : base()
    {

    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        Session session = GameManager.instance.ActiveSession;
        SessionParameters sessionParams = session.SessionParams;

        // Resize array
        m_DisplayedSyllables = new SyllableViewModel[sessionParams.SyllableSearchedAmount];

        // Fill UI Elements from model
        ICryptoSyllable[] displayedSyllableList = session.GetLastInputSyllables();
        Debug.Assert(displayedSyllableList.Length == m_DisplayedSyllables.Length, "WHY WOULD YOU DO THAT!?");

        for (int i = 0; i < m_DisplayedSyllables.Length; ++i)
        {
            // get model data
            var displayedSyllable = displayedSyllableList[i];

            // Update view model with model data
            m_DisplayedSyllables[i] = new SyllableViewModel();
            m_DisplayedSyllables[i].SetFromSyllable(displayedSyllable);
        }
    }

    private SyllableViewModel[] m_DisplayedSyllables;
}
