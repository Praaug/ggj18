/// <summary>
/// Wrapper class for sprite syllables
/// </summary>
[System.Serializable]
public class CryptoSyllableSprite : ICryptoSyllable
{
    [UnityEngine.SerializeField]
    private UnityEngine.Sprite syllable;

    public object GetSyllable()
    {
        return syllable;
    }
}
