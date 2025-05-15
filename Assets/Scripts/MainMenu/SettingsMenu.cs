using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private Button resetButton;
    [SerializeField] private LevelSelectUI levelSelectUI;

    void Awake()
    {
        // Always remove any old listeners, then add ours
        resetButton.onClick.RemoveAllListeners();
        resetButton.onClick.AddListener(OnResetClicked);
    }

    private void OnResetClicked()
    {
        ProgressManager.Instance.ResetProgress();

        // If you have a LevelSelectUI already in the scene, refresh it:
        if (levelSelectUI != null) 
        {
            levelSelectUI.LoadButtons();
            Debug.Log("LoadButtons");
        }
        Debug.Log("OnResetClicked");

    }
}
