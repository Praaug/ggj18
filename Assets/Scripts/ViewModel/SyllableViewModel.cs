using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SyllableViewModel : BaseViewModel
{
    #region Public Events
    public event Action<Sprite> OnIconImageChanged;
    public event Action<string> OnIconTextChanged;
    public event Action OnIconHide;
    #endregion

    #region Public Properties
    public override MenuEnum MenuType => (MenuEnum)(-1);

    public Sprite IconImage
    {
        get { return m_IconImage; }
        set
        {
            if (m_IconImage == value)
            {
                return;
            }

            m_IconImage = value;
            OnIconImageChanged?.Invoke(m_IconImage);
        }
    }

    public string IconText
    {
        get { return m_IconText; }
        set
        {
            if (m_IconText == value)
            {
                return;
            }

            m_IconText = value;
            OnIconTextChanged?.Invoke(m_IconText);
        }
    }
    #endregion

    #region Public Methods
    public void SetFromSyllable(ICryptoSyllable syllable)
    {
        object syllableContent = syllable.GetSyllable();

        if (syllableContent is Sprite)
        {
            SetImage((Sprite)syllableContent);
        }
        else if (syllableContent is string)
        {
            SetText((string)syllableContent);
        }

    }

    public void SetImage(Sprite sprite)
    {
        Debug.Assert(sprite != null, "You should not set the sprite to manually to null");
        m_IconText = null;
        IconImage = sprite;
    }

    public void SetText(string text)
    {
        Debug.Assert(!string.IsNullOrEmpty(text), "You should not set the text of a syllable to an empty string");
        m_IconImage = null;
        IconText = text;
    }

    public override void OnExitState()
    {
        OnIconHide?.Invoke();
    }
    #endregion

    #region Private Members
    private Sprite m_IconImage = null;
    private string m_IconText = null;
    #endregion
}
