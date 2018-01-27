using UnityEngine;

/// <summary>
/// Asset representation of a text based language
/// </summary>
[CreateAssetMenu(fileName = "TextLanguage", menuName = "Language/Text")]
public class CryptoLanguageText : ACryptoLanguage<CryptoSyllableText>
{
    #region Public Properties
    public override LanguageType Type { get { return LanguageType.Text; } }
    #endregion

    #region Public Methods
    /// <summary>
    /// Retroactively sets the language's syllables
    /// </summary>
    /// <param name="syllables">The syllables to set</param>
    public void SetSyllables(System.Collections.Generic.List<string> syllables)
    {
        this.syllables = new CryptoSyllableText[syllables.Count];
        for (int i = 0; i < syllables.Count; i++)
        {
            this.syllables[i] = new CryptoSyllableText(syllables[i]);
        }
    } 
    #endregion
}
