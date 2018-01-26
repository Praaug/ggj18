using UnityEditor;
using UnityEngine;

/// <summary>
/// Editor utility class forcreating language assets
/// </summary>
public static class LanguageCreator
{

    #region Private Methods

    /// <summary>
    /// Creates a text language
    /// </summary>
    [MenuItem("Assets/Create/Languages/Text Language")]
    private static void CreateTextLanguage() => CreateLanguageAsset<CryptoLanguageText>();

    /// <summary>
    /// Creates an image language
    /// </summary>
    [MenuItem("Assets/Create/Languages/Image Language")]
    private static void CreateImageLanguage() => CreateLanguageAsset<CryptoLanguageSprite>();

    /// <summary>
    /// Creates a sound language
    /// </summary>
    [MenuItem("Assets/Create/Languages/Sound Language")]
    private static void CreateSoundLanguage() => CreateLanguageAsset<CryptoLanguageSound>();

    /// <summary>
    /// Creates a language asset in the currently selected Folder
    /// </summary>
    /// <typeparam name="T">The language type</typeparam>
    private static void CreateLanguageAsset<T>() where T : ACryptoLanguage
    {
        T instance = ScriptableObject.CreateInstance<T>();

        string path = AssetDatabase.GetAssetPath(Selection.activeObject);
        if (!AssetDatabase.IsValidFolder(path))
        {
            Debug.LogError("Please select a folder to create the language in");
            return;
        }

        string newPath = System.IO.Path.Combine(path, "New Language.asset");
        AssetDatabase.CreateAsset(instance, AssetDatabase.GenerateUniqueAssetPath(newPath));
    }
    #endregion

}
