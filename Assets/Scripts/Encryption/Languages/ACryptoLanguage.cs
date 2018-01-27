using UnityEngine;
using System.Linq;

/// <summary>
/// Asset representation of an abstract language
/// </summary>
public abstract class ACryptoLanguage : ScriptableObject
{
    #region Public Properties

    /// <summary>
    /// Returns the language's type
    /// </summary>
    public abstract LanguageType Type { get; }

    /// <summary>
    /// Returns this language's number of syllables
    /// </summary>
    public abstract int syllableCount { get; }

    #endregion

    #region Public Methods
    /// <summary>
    /// Returns this language's syllables
    /// </summary>
    /// <returns></returns>
    public abstract ICryptoSyllable[] GetSyllables();
    #endregion
}

/// <summary>
/// Asset representation of an abstract generic language
/// </summary>
/// <typeparam name="T">The type of syllables this language is made up of</typeparam>
public abstract class ACryptoLanguage<T> : ACryptoLanguage where T : ICryptoSyllable
{
    #region Protected Fields
    /// <summary>
    /// The syllables this language is comprised of
    /// </summary>
    [SerializeField]
    protected T[] syllables;
    #endregion

    #region Public Properties

    public override int syllableCount { get { return syllables.Length; } }

    #endregion

    #region Public Methods
    public override ICryptoSyllable[] GetSyllables() { return syllables.Cast<ICryptoSyllable>().ToArray(); }
    #endregion
}

public enum LanguageType
{
    Text,
    Image,
    Sound,
    Color
}