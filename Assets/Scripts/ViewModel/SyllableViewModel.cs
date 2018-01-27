using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class SyllableViewModel
{
    #region Public Events
    public event Action<Image> OnIconImageChanged;
    public event Action<Text> OnIconTextChanged;
    #endregion

    #region Public Properties
    public Image IconImage
    {
        get { return m_IconImage; }
        set
        {
            if (m_IconImage == value)
            {
                return;
            }

            m_IconImage = value;
            OnIconImageChanged(m_IconImage);
        }
    }

    public Text IconText
    {
        get { return m_IconText; }
        set
        {
            if (m_IconText == value)
            {
                return;
            }

            m_IconText = value;
            OnIconTextChanged(m_IconText);
        }
    }
    #endregion

    #region Public Methods
    public void SetImage(Sprite sprite)
    {
        Debug.Assert(sprite != null, "You should not set the sprite to manually to null");

        // Disable text
        m_IconText.gameObject.SetActive(false);

        // Replace sprite in image
        m_IconImage.gameObject.SetActive(true);
        m_IconImage.sprite = sprite;
    }

    public void SetText(string text)
    {
        Debug.Assert(!string.IsNullOrEmpty(text), "You should not set the text of a syllable to an empty string");

        // Disable image
        m_IconImage.gameObject.SetActive(false);

        // Replace text in image
        m_IconText.gameObject.SetActive(true);
        m_IconText.text = text;
    }
    #endregion

    #region Private Members
    private Image m_IconImage = null;
    private Text m_IconText = null;
    #endregion
}
