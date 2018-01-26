using UnityEngine;
using System.Collections;

/// <summary>
/// Class that contains all 
/// </summary>
public class SaveGame
{
    public string Name { get; set; }

    public int CurrentRound { get; set; }

    public int MaxRounds { get; set; }

    public SaveGame()
    {
        Name = "testing";
        CurrentRound = Random.Range(0, 5);
        MaxRounds = Random.Range(CurrentRound, 5);
    }
}
