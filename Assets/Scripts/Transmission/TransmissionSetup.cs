/// <summary>
/// Wrapper class for Transmission setup data
/// </summary>
public class TransmissionSetup
{
    #region Public Properties
    /// <summary>
    /// The word the first player gets to see
    /// </summary>
    public TransmissionWord StartWord { get; private set; }

    /// <summary>
    /// The final word and language
    /// </summary>
    public TransmissionEndpoint EndPoint { get; private set; }

    /// <summary>
    /// The transmissions to go through
    /// </summary>
    public Transmission[] Transmissions { get; private set; }
    #endregion

    #region Public Methods

    public TransmissionSetup(TransmissionWord start, TransmissionEndpoint end, Transmission[] transmissions)
    {
        StartWord = start;
        EndPoint = end;
        Transmissions = transmissions;
    }

    #endregion
}
