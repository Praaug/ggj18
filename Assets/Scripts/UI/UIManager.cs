using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UIManager
{
    #region Public Methods
    public void Init()
    {
        foreach (UIScene scene in m_SceneList)
        {
            m_SceneDict.Add(scene.State, scene);
        }
    }
    #endregion

    #region Private Members
    [SerializeField]
    private List<UIScene> m_SceneList = new List<UIScene>();

    private Dictionary<UIState, UIScene> m_SceneDict = new Dictionary<UIState, UIScene>();
    #endregion
}
