using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager gameManager => target as GameManager;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // Debug buttons

        m_FoldoutSettings = EditorGUILayout.Foldout(m_FoldoutSettings, "Game settings");
        if (m_FoldoutSettings)
        {
            parameter.SessionName = EditorGUILayout.TextField("Session Name", parameter.SessionName);
            parameter.RoundCount = (byte)EditorGUILayout.IntField("Round Count", parameter.RoundCount);
            parameter.SyllableChoiceAmount = EditorGUILayout.IntField("Syllable Choice Amount", parameter.SyllableChoiceAmount);
            parameter.SyllableSearchedAmount = EditorGUILayout.IntField("Syllable Searched Amount", parameter.SyllableSearchedAmount);

            GUILayout.BeginHorizontal();
            parameter.Seed = EditorGUILayout.IntField("Seed", parameter.Seed);
            if (GUILayout.Button("New", EditorStyles.miniButton, GUILayout.Width(50)))
            {
                parameter.Seed = Random.Range(int.MinValue, int.MaxValue);
            }
            GUILayout.EndHorizontal();

        }

        GUILayout.Space(16.0f); // some fancy empty space to seperate buttons from input fields

        if (GUILayout.Button("StartNewGame"))
        {
            gameManager.StartNewGame(parameter);
        }

        GUILayout.Space(64.0f); // some fancy empty space to seperate buttons from input fields

        if (gameManager.ActiveSession != null)
        {
            GUILayout.Label("Session Info", EditorStyles.boldLabel);
        }

        // Debug texts



        // Serialization flag
        if (GUI.changed)
        {
            EditorUtility.SetDirty(this);
        }
    }

    [SerializeField]
    private bool m_FoldoutSettings = false;

    [SerializeField]
    private SessionParameters parameter = new SessionParameters();
}
