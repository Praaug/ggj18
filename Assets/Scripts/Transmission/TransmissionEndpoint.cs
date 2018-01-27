using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// The endpoint of a transmission. Contains the generated human language and the unencrypted word
/// </summary>
public class TransmissionEndpoint
{
    /// <summary>
    /// The real word
    /// </summary>
    public TransmissionWord RealWord { get; private set; }

    /// <summary>
    /// The generated human language excerpt
    /// </summary>
    public LanguageExcerpt HumanLanguage { get; private set; }

    public TransmissionEndpoint(LanguageExcerpt humanLanguage, TransmissionWord startWord)
    {
        HumanLanguage = humanLanguage;
        RealWord = startWord;
    }
}
