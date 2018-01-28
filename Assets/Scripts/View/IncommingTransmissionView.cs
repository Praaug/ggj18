using System.Collections.Generic;
using UnityEngine;

public class IncommingTransmissionView : BaseView<IncommingTransmissionViewModel>
{
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        base.m_viewModel = GameViewModel.instance.IncommingTransmissionViewModel;
        Debug.Assert(base.m_viewModel != null, "OptionsViewModel not valid");
        base.Init(base.m_viewModel);

        Debug.Log("Registering OnDisplaySyllablesCountChanged");
        ViewModelConcrete.OnDisplaySyllablesCountChanged += ViewModel_OnDisplaySyllablesCountChanged;
    }

    private void ViewModel_OnDisplaySyllablesCountChanged()
    {
        var syllablesViewModelList = ViewModelConcrete.GetDisplayedSyllables();

        for (int i = 0; i < m_SyllableList.Count; ++i)
        {
            SyllableView syllableView = m_SyllableList[i];

            if (i < syllablesViewModelList.Length)
            {
                SyllableViewModel syllableViewModel = syllablesViewModelList[i];
                syllableView.gameObject.SetActive(true);
                syllableView.Init(syllableViewModel);
            }
            else
            {
                syllableView.gameObject.SetActive(false);
            }
        }

        // todo: Change this abomination
        Invoke("FinishTimer", GameManager.instance.ActiveSession.LastWordDisplayTime);
    }

    private void FinishTimer()
    {
        if (m_viewModel != null)
            ViewModelConcrete.TimerFinishCommand();
        else
            Debug.Log("Could not finish timer, view model missing");
    }

    #region Private Members
    [SerializeField]
    private List<SyllableView> m_SyllableList = new List<SyllableView>();
    #endregion
}
