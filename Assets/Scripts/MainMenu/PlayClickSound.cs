using UnityEngine;
using UnityEngine.UI;
using static AudioManager;

public class PlayClickSound : MonoBehaviour
{
    public bool isSelectButton = true;
    void Start()
    {
        Button button = this.GetComponent<Button>();
        if (button != null && AudioManager.Instance != null && isSelectButton)
        {
            button.onClick.AddListener(() =>
                            AudioManager.Instance.PlayUISound(AudioManager.Sound.ButtonSelect));
        }
        else 
        {
            button.onClick.AddListener(() =>
                            AudioManager.Instance.PlayUISound(AudioManager.Sound.ButtonCancel));
        }
    }
}