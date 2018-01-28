[System.Serializable]
public class SessionParameters
{
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
    public int SyllableChoiceAmount = 6;

    /// <summary>
    /// The duration of the incomming syllables
    /// </summary>
    public float LastWordDisplayTime = 3.0f;
}
