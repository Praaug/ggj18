using System;
using UnityEngine;

public class IncommingTransmissionViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.IncommingTransmission;

    public event Action OnDisplaySyllablesCountChanged;

    public event Action OnWaitTimePassed;

    public event Action OnExitStateAction;

    public IncommingTransmissionViewModel(GameViewModel gameViewModel) : base(gameViewModel)
    {

    }

    public SyllableViewModel[] GetDisplayedSyllables()
    {
        return m_DisplayedSyllables;
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
            var syllableViewModel = new SyllableViewModel();
            syllableViewModel.SetFromSyllable(displayedSyllable);

            m_DisplayedSyllables[i] = syllableViewModel;
        }

        Debug.Log("OnDisplaySyllablesCountChanged");
        OnDisplaySyllablesCountChanged?.Invoke();
    }

    public override void OnExitState()
    {
        base.OnExitState();
    }

    public void TimerFinishCommand()
    {
        OnWaitTimePassed?.Invoke();
    }

    private SyllableViewModel[] m_DisplayedSyllables;
}
