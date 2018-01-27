using System;
using System.Collections.Generic;

public class MainMenuViewModel : BaseViewModel
{
    public override MenuEnum MenuType => MenuEnum.MainMenu;

    public List<SaveGameViewModel> SaveGameViewModelList { get; set; }

    public event Action OnOpenNewGameCommand;

    public event Action OnOpenOptionsCommand;

    public MainMenuViewModel() : base()
    {
        var saveGameList = GameManager.instance.SaveGameList;
        SaveGameViewModelList = new List<SaveGameViewModel>(saveGameList.Count);

        for (int i = 0; i < saveGameList.Count; i++)
        {
            SaveGameViewModelList.Add(new SaveGameViewModel(saveGameList[i], i));
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

    internal void OpenWebsiteCommand()
    {
        UnityEngine.Application.OpenURL("https://globalgamejam.org/2018/games/whisper-down-lane");
    }
}
