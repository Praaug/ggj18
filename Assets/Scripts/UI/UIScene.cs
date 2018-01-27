using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UIScene
{
    #region Public Properties
    public Scene Scene => m_scene;
    public UIState State => m_state;
    #endregion

    #region Public Methods
    [SerializeField]
    private Scene m_scene;

    [SerializeField]
    private UIState m_state = UIState.MainMenu;
    #endregion
}
