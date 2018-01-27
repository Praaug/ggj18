using System;

class NewGameViewModel : BaseViewModel
{
    public NewGameViewModel() : base()
    {

    }

    public override void CloseButtonCommand()
    {
        UnityEngine.Debug.Log("Close NewGame Command");
        // Todo: model logic
    }

    public void StartGameCommand()
    {
        UnityEngine.Debug.Log("Start New Command");
        // Todo: model logic
    }
}
