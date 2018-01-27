using System;

public class IncommingTransmissionViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.IncommingTransmission;

    public IncommingTransmissionViewModel() : base()
    {

    }

    public override void OnEnterState()
    {
        base.OnEnterState();

        // Fill UI Elements from model
        ICryptoSyllable[] displayedSyllables = GameManager.instance.ActiveSession.GetLastInputSyllables();


    }
}
