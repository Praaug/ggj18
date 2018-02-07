using System;

[System.Serializable]
public class SessionParameters
{
	/// <summary>
	/// The guid of the session used to identify this session instance globally
	/// </summary>
	public string guid = Guid.Empty.ToString();

	/// <summary>
	/// The seed used to determine the transmission setup
	/// </summary>
	public int Seed = 0;

	/// <summary>
	/// The number of rounds int the session
	/// </summary>
	public byte RoundCount = 3;

	/// <summary>
	/// The name of the current session
	/// </summary>
	public string SessionName = "SuperDuperSession";

	/// <summary>
	/// The amount of syllables of the searched word
	/// </summary>
	public int SyllableSearchedAmount = 3;

	/// <summary>
	/// 
	/// </summary>
	public int SyllableChoiceAmount = 9;

	/// <summary>
	/// The duration of the incomming syllables
	/// </summary>
	public float LastWordDisplayTime = 3.0f;
}
