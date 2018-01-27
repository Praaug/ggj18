using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyllableView : BaseView<SyllableViewModel>
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

    public override void Init(BaseViewModel viewModel)
    {
        base.Init(viewModel);

        var syllableViewModel = (SyllableViewModel)m_viewModel;
        syllableViewModel.OnIconImageChanged += SyllableViewModel_OnIconImageChanged;
        syllableViewModel.OnIconTextChanged += SyllableViewModel_OnIconTextChanged;
    }
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        m_IconText.gameObject.SetActive(false);
        m_IconImage.gameObject.SetActive(false);
    }
    #endregion

    #region Private Methods
    private void SyllableViewModel_OnIconTextChanged(string text)
    {
        SetText(text);
    }

    private void SyllableViewModel_OnIconImageChanged(Sprite image)
    {
        SetImage(image);
    }
    #endregion

    #region Private Members
    [SerializeField]
    private Image m_IconImage = null;
    [SerializeField]
    private Text m_IconText = null;
    #endregion
}
