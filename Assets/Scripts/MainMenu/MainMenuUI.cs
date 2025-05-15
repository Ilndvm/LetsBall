using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject levelPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;
    public GameObject questionsPanel;

    [Header("Start Button Settings")]
    public string levelToLoad = "GameScene"; // Name of the level to load

    void Start()
    {
        ShowMainMenu();
    }

    void Update()
    {
        // If not on main menu and Escape is pressed go back
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!mainMenuPanel.activeSelf)
            {
                ShowMainMenu();
            }
        }
    }

    // Called by Start button
    public void OnStartButton()
    {
        mainMenuPanel.SetActive(false);
        levelPanel.SetActive(true);
    }

    // Called by Settings button
    public void OnSettingsButton()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // Called by Credits button
    public void OnCreditsButton()
    {
        mainMenuPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    // Called by Questions button
    public void OnQuestionsButton()
    {
        mainMenuPanel.SetActive(false);
        questionsPanel.SetActive(true);
    }

    // Called by Back button on any sub-panel
    public void OnBackButton()
    {
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        levelPanel.SetActive(false);
        settingsPanel.SetActive(false);
        creditsPanel.SetActive(false);
        questionsPanel.SetActive(false);
    }
}
