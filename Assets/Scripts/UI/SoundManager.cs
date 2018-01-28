using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public SoundManager instance => new SoundManager();

    public SoundManager()
    {

    }

    public void Init(GameViewModel gameViewModel)
    {
        gameViewModel.OnUpdateMenu += GameViewModel_OnUpdateMenu;
    }

    private void GameViewModel_OnUpdateMenu(BaseViewModel newModel, BaseViewModel oldModel)
    {
        
    }

    [SerializeField]
    private AudioSource m_mainMenuSource;

    [SerializeField]
    private AudioSource m_ingameSource;

    [SerializeField]
    private float m_fadeTime;
}
