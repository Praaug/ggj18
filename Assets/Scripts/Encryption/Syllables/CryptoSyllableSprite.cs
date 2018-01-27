/// <summary>
/// Wrapper class for sprite syllables
/// </summary>
[System.Serializable]
public class CryptoSyllableSprite : ICryptoSyllable
{
    #region Private Fields
    [UnityEngine.SerializeField]
    private UnityEngine.Sprite syllable;
    #endregion

    #region Public Methods
    public object GetSyllable()
    {
        return syllable;
    } 
    #endregion
}
