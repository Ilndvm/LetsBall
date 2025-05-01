using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlockButton : MonoBehaviour
{
    #region Inspector
    public Image iconImage;    // assign child Icon Image
    public TMP_Text countText;    // assign child TextMeshPro text
    #endregion

    #region Private
    private Button _button;
    private BuildManager _buildManager;
    private int _typeIndex;
    #endregion

    #region Initialization
    /// <summary>
    /// Called by BuildManager to set up this button.
    /// </summary>
    public void Init(BuildManager bm, int typeIndex, Sprite icon, int startCount)
    {
        _buildManager = bm;
        _typeIndex = typeIndex;

        // cache button component & hook click
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        // set icon + initial count
        iconImage.sprite = icon;
        UpdateCount(startCount);
    }
    #endregion

    #region Public API
    public void UpdateCount(int newCount)
    {
        countText.text = newCount.ToString();
        bool hasBlocks = newCount > 0;

        // clickable only if >0
        _button.interactable = hasBlocks;
        // icon tinted gray if none left
        iconImage.color = hasBlocks
            ? Color.white
            : Color.gray;
    }
    #endregion

    #region Event Handlers
    private void OnClick()
    {
        _buildManager.SelectBlockType(_typeIndex);
    }
    #endregion
}
