using System;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.MainMenu;

    public List<SaveGameViewModel> SaveGameViewModelList { get; set; }

    public event Action OnOpenNewGameCommand;

    public event Action OnOpenOptionsCommand;

    public event Action<int> OnLoadGameCommand;

    public MainMenuViewModel(GameViewModel gameViewModel) : base(gameViewModel)
    {
        var saveGameList = GameManager.instance.SaveGameList;
        SaveGameViewModelList = new List<SaveGameViewModel>(saveGameList.Count);

        for (int i = 0; i < saveGameList.Count; i++)
        {
            SaveGameViewModel saveGameViewModel = new SaveGameViewModel(saveGameList[i]);
            saveGameViewModel.Index = i;
            saveGameViewModel.OnLoadSaveGameCommand += SaveGameViewModel_OnLoadSaveGameCommand;

            SaveGameViewModelList.Add(saveGameViewModel);
        }
    }

    public void OpenNewGameCommand()
    {
        OnOpenNewGameCommand?.Invoke();
    }

    public void OpenOptionsCommand()
    {
        OnOpenOptionsCommand?.Invoke();
    }

    public override void CloseButtonCommand()
    {
        UnityEngine.Application.Quit();
    }

    public void OpenWebsiteCommand()
    {
        UnityEngine.Application.OpenURL("https://globalgamejam.org/2018/games/whisper-down-lane");
    }

    private void SaveGameViewModel_OnLoadSaveGameCommand(SaveGameViewModel saveGameViewModel)
    {
        int index = saveGameViewModel.Index;
        bool validIndex = index >= 0 && index < SaveGameViewModelList.Count;
        Debug.Assert(validIndex, string.Format("Callback of save game view model with invalid index {0}", index));
        if (!validIndex)
        {
            return;
        }

        // Load save game in model with index
        OnLoadGameCommand?.Invoke(index);
    }

}
