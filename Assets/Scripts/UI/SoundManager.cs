using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public void Start()
    {
        m_ingameSource.volume = 0.0f;
        GameViewModel.instance.OnUpdateMenu += GameViewModel_OnUpdateMenu;
    }

    private void GameViewModel_OnUpdateMenu(BaseViewModel newModel, BaseViewModel oldModel)
    {
        if (newModel.MenuType == MenuEnum.IncommingTransmission || newModel.MenuType == MenuEnum.SyllableInput)
        {
            if (isInGame)
            {
                return;
            }

            if (m_fadeCoroutine != null)
            {
                StopCoroutine(m_fadeCoroutine);
            }

            m_fadeCoroutine = StartCoroutine(Coroutine_FadeMusic(m_mainMenuSource, m_ingameSource));

            isInGame = true;
        }
        else
        {
            if (!isInGame)
            {
                return;
            }

            if (m_fadeCoroutine != null)
            {
                StopCoroutine(m_fadeCoroutine);
            }

            m_fadeCoroutine = StartCoroutine(Coroutine_FadeMusic(m_ingameSource, m_mainMenuSource));

            isInGame = false;
        }
    }

    [SerializeField]
    private AudioSource m_mainMenuSource;

    [SerializeField]
    private AudioSource m_ingameSource;

    [SerializeField]
    private float m_fadeTime;

    private Coroutine m_fadeCoroutine;

    private bool isInGame = false;

    private IEnumerator Coroutine_FadeMusic(AudioSource oldSource, AudioSource newSource)
    {
        float _time = 0.0f;
        while (_time < m_fadeTime)
        {
            _time += Time.deltaTime;

            oldSource.volume = 1 - (_time / m_fadeTime);
            newSource.volume = _time / m_fadeTime;

            yield return null;
        }

        m_fadeCoroutine = null;
    }
}
