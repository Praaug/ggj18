
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(WordManager))]
public class WordManagerEditor : Editor
{
    private WordManager wordManager => target as WordManager;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (GUILayout.Button("Add word"))
        {
            ArrayUtility.Add(ref wordManager.wordList, new Word());
        }
    }
}
