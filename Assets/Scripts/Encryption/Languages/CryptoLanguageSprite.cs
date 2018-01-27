using UnityEngine;

/// <summary>
/// Asset representation of an image based language
/// </summary>
[CreateAssetMenu(fileName = "ImageLanguage", menuName = "Language/Image")]
public class CryptoLanguageSprite : ACryptoLanguage<CryptoSyllableSprite>
{
    #region Public Properties
    public override LanguageType Type { get { return LanguageType.Image; } }
    #endregion
}
