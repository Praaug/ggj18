using System;

/// <summary>
/// Represents a single running instance of a game
/// </summary>
public class Session
{
    #region Public Methods
    public string SessionName { get; } = "Test";

    /// <summary>
    /// Returns the save game info of this game instance
    /// </summary>
    public SaveGame CreateSaveGame()
    {
        return null;
    }
    #endregion
}
