using UnityEngine;

/// <summary>
/// Asset representation of a sound based language
/// </summary>
[CreateAssetMenu(fileName = "ColorLanguage", menuName = "Language/Color")]
public class CryptoLanguageColor : ACryptoLanguage<CryptoSyllableColor>
{
    #region Public Properties
    public override LanguageType Type { get { return LanguageType.Color; } }
    #endregion
}
