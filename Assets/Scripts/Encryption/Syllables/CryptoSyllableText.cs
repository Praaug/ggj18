/// <summary>
/// Wrapper class for text syllables
/// </summary>
[System.Serializable]
public class CryptoSyllableText : ICryptoSyllable
{
	#region Private Fields
	[UnityEngine.SerializeField]
	private string syllable;
	#endregion

	#region Public Methods
	public object GetSyllable()
	{
		return syllable;
	} 
	#endregion
}
