using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipManager : MonoBehaviour
{
    public static TooltipManager Instance { get; private set; }

    [Header("Tooltip UI")]
    [SerializeField] private GameObject tooltipObject;   // root panel
    [SerializeField] private TMP_Text headerText;        // header TMP
    [SerializeField] private TMP_Text descriptionText;   // body TMP
    [SerializeField] private Vector2 margin = new Vector2(15, 15);

    private RectTransform _rt;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else { Destroy(gameObject); return; }

        _rt = tooltipObject.GetComponent<RectTransform>();
        tooltipObject.SetActive(false);
    }

    void Update()
    {
        if (!tooltipObject.activeSelf) return;

        Vector2 mousePos = Input.mousePosition;
        // pivot logic: choose corner based on mouse position
        Vector2 pivot = new Vector2(
            mousePos.x / Screen.width > 0.5f ? 1 : 0,
            mousePos.y / Screen.height > 0.5f ? 1 : 0
        );
        _rt.pivot = pivot;

        Vector2 offset = new Vector2(
            pivot.x == 1 ? -margin.x : margin.x,
            pivot.y == 1 ? -margin.y : margin.y
        );
        _rt.position = mousePos + offset;
    }

    /// Show tooltip with a header and a description.
    public void ShowTooltip(string header, string description)
    {
        headerText.text = header;
        descriptionText.text = description;

        // Force the layout to rebuild so the panel resizes correctly
        LayoutRebuilder.ForceRebuildLayoutImmediate(_rt);

        tooltipObject.SetActive(true);
    }

    public void HideTooltip()
    {
        tooltipObject.SetActive(false);
    }
}
