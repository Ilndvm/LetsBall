using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelButtonUI : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text levelLabel;
    [SerializeField] private GameObject[] starIcons;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private Sprite completedStarSprite;
    [SerializeField] private Sprite defaultStarSprite;
    [SerializeField] private Color defaultButtonColor = Color.white;
    [SerializeField] private Color perfectButtonColor = Color.yellow;

    private int levelIndex; 

    public void Setup(int levelIdx, bool unlocked, int stars)
    {
        levelIndex = levelIdx;
        levelLabel.text = $"{levelIdx}";

        button.interactable = unlocked;
        lockIcon.SetActive(!unlocked);
        levelLabel.gameObject.SetActive(unlocked);

        foreach (var star in starIcons)
            star.SetActive(unlocked);

        if (unlocked)
        {
            for (int i = 0; i < starIcons.Length; i++)
            {
                Image img = starIcons[i].GetComponent<Image>();
                img.sprite = (i < stars) ? completedStarSprite : defaultStarSprite;
            }

            button.GetComponent<Image>().color =
                (stars >= starIcons.Length)
                ? perfectButtonColor
                : defaultButtonColor;

            button.onClick.RemoveAllListeners();
            button.onClick.AddListener(() =>
                UnityEngine.SceneManagement.SceneManager.LoadScene(levelIndex)
            );
        }
    }
}
