using UnityEngine;
using System.Linq;

public abstract class ACryptoLanguage : ScriptableObject
{
    public abstract ICryptoSyllable[] GetSyllables();
}

public abstract class ACryptoLanguage<T> : ACryptoLanguage where T : ICryptoSyllable
{
    [SerializeField]
    private T[] syllables;

    public override ICryptoSyllable[] GetSyllables() { return syllables.Cast<ICryptoSyllable>().ToArray(); }
}