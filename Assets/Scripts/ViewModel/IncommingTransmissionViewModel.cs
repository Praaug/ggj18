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
        ICryptoSyllable[] displayedSyllableList = GameManager.instance.ActiveSession.GetLastInputSyllables();
        Debug.Assert(displayedSyllableList.Length == m_DisplayedSyllables.Length, "WHY WOULD YOU DO THAT!?");

        for (int i = 0; i < m_DisplayedSyllables.Length; ++i)
        {
            var displayedSyllable = displayedSyllableList[i];

            object syllableContent = displayedSyllable.GetSyllable();

            if (syllableContent is Sprite)
            {
                m_DisplayedSyllables[i].SetImage((Sprite)syllableContent);
            }
            else if (syllableContent is string)
            {
                m_DisplayedSyllables[i].SetText((string)syllableContent);
            }
        }
    }

    private SyllableViewModel[] m_DisplayedSyllables;
}
