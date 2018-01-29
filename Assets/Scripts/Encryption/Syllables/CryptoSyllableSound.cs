/// <summary>
/// Wrapper class for sound syllables
/// </summary>
[System.Serializable]
public class CryptoSyllableSound : ICryptoSyllable
{
	#region Private Fields

	/// <summary>
	/// The syllable AudioClip
	/// </summary>
	[UnityEngine.SerializeField]
	private UnityEngine.AudioClip syllable = null;

	#endregion

	#region Public Methods

	public object GetSyllable()
	{
		return syllable;
	} 
	
	#endregion
}
