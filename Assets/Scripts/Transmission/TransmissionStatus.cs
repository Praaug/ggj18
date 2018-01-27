/// <summary>
/// Data representation of the session's current state
/// </summary>
[System.Serializable]
public struct TransmissionStatus
{
    #region Public Fields
    /// <summary>
    /// The game session's start seed
    /// </summary>
    public int Seed;

    /// <summary>
    /// The game session's current transition index
    /// </summary>
    public byte TransmissionIndex;

    /// <summary>
    /// The total amount of transmissions
    /// </summary>
    public byte TransmissionCount;

    /// <summary>
    /// The current code word
    /// </summary>
    /// <remarks>Not deterministic because player dependent</remarks>
    public TransmissionWord CurrentWord;

    /// <summary>
    /// The original code word
    /// </summary>
    public TransmissionWord OriginalWord; 
    #endregion
}
