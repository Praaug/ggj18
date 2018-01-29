/// <summary>
/// Wrapper class for sound syllables
/// </summary>
[System.Serializable]
public class CryptoSyllableColor : ICryptoSyllable
{
    #region Private Fields

    /// <summary>
    /// The syllable AudioClip
    /// </summary>
    [UnityEngine.SerializeField]
    private UnityEngine.Color syllable = UnityEngine.Color.white;

    #endregion

    #region Public Methods

    public object GetSyllable()
    {
        return syllable;
    }

    #endregion
}
