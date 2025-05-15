using UnityEngine;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject levelButtonPrefab;
    [SerializeField] private Transform buttonParent;

    void Start()
    {
        LoadButtons();
    }

    public void LoadButtons()
    {
        for (int i = buttonParent.childCount - 1; i >= 0; i--)
        {
            Destroy(buttonParent.GetChild(i).gameObject);
        }

        var pm = ProgressManager.Instance;
        for (int i = 0; i < pm.TotalLevels; i++)
        {
            GameObject go = Instantiate(levelButtonPrefab, buttonParent);
            var btnUI = go.GetComponent<LevelButtonUI>();
            bool unlocked = (i + 1) <= pm.UnlockedLevel;
            int stars = pm.GetStars(i + 1);
            btnUI.Setup(i + 1, unlocked, stars);
        }
    }
}
