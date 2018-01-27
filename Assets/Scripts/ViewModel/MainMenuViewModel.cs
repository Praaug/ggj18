using System;
using System.Collections.Generic;

public class MainMenuViewModel : BaseViewModel
{
    public List<SaveGameViewModel> SaveGameViewModelList { get; set; }

    public MainMenuViewModel() : base()
    {
        List<SaveGame> saveGameList = GameManagerMock.GetSavegames();
        SaveGameViewModelList = new List<SaveGameViewModel>(saveGameList.Count);

        for (int i = 0; i < saveGameList.Count; i++)
        {
            SaveGameViewModelList.Add(new SaveGameViewModel(saveGameList[i], i));
        }
    }

    public void StartNewGameCommand()
    {
        UnityEngine.Debug.Log("Start new game command");
        // Todo: model logic
    }

    public void OpenOptionsCommand()
    {
        UnityEngine.Debug.Log("open options command");
        // Todo: model logic
    }

    public override void CloseButtonCommand()
    {
        UnityEngine.Debug.Log("close app command");
        // Todo: model logic
    }

    internal void OpenWebsiteCommand()
    {
        UnityEngine.Debug.Log("open app command");
        // Todo: model logic
    }
}
