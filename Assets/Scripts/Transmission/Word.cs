using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Representation of a human language word
/// </summary>
[System.Serializable]
public class Word
{
    public string[] syllables;

    [System.NonSerialized]
    public int[] syllableIndices;
}
