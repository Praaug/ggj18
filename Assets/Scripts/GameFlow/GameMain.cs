using UnityEngine;
using System.Collections;

public class GameMain : MonoBehaviour
{
    #region Unity Callbacks
    private void Awake()
    {
        if (m_gameManager == null)
        {
            Debug.LogAssertion("No GameManager object assigned for the game main prefab");
            return;
        }

        DontDestroyOnLoad(gameObject);

        m_gameManager.Init();
    }
    #endregion

    #region Private Members 
    [SerializeField]
    private GameManager m_gameManager = null;
    #endregion
}
