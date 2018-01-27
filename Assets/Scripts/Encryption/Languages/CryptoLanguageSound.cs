using UnityEngine;

/// <summary>
/// Asset representation of a sound based language
/// </summary>
[CreateAssetMenu(fileName = "SoundLanguage", menuName = "Language/Sound")]
public class CryptoLanguageSound : ACryptoLanguage<CryptoSyllableSound>
{
    #region Public Properties
    public override LanguageType Type { get { return LanguageType.Sound; } }
    #endregion
}
