using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BlockButton : MonoBehaviour,
    IPointerEnterHandler, IPointerExitHandler
{
    public Image iconImage;
    public TMP_Text countText;

    private Button _button;
    private BuildManager _buildManager;
    private int _typeIndex;
    private string _header;
    private string _description;

    public void Init(BuildManager bm, int typeIndex, Sprite icon, int startCount, string header, string description)
    {
        _buildManager = bm;
        _typeIndex = typeIndex;
        _header = header;
        _description = description;

        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnClick);

        iconImage.sprite = icon;
        UpdateCount(startCount);
    }

    public void UpdateCount(int newCount)
    {
        countText.text = newCount.ToString();
        bool has = newCount > 0;
        _button.interactable = has;
        iconImage.color = has ? Color.white : Color.gray;
    }

    void OnClick()
    {
        _buildManager.SelectBlockType(_typeIndex);
        AudioManager.Instance.PlaySound(AudioManager.Sound.BlockChoice);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_button.interactable)
            TooltipManager.Instance.ShowTooltip(_header, _description);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipManager.Instance.HideTooltip();
    }
}
