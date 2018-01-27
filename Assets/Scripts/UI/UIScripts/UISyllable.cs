﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISyllable : MonoBehaviour
{
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

    #region Unity Callbacks
    private void Awake()
    {
        m_IconText.gameObject.SetActive(false);
        m_IconImage.gameObject.SetActive(false);
    }
    #endregion

    #region Private Members
    [SerializeField]
    private Image m_IconImage = null;
    [SerializeField]
    private Text m_IconText = null;
    #endregion
}