// UIManager.cs
using UnityEngine;
using UnityEngine.SceneManagement;

// Manages overall UI: build UI, pause, win screen, start/stop.
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    public GameObject gameplayUI;  // build-phase UI root
    public GameObject winPanel;    // shown on win
    public GameObject pausePanel;  // pause menu
    public GameObject startButton;
    public GameObject stopButton;

    bool uiHidden = false;
    bool isPaused = false;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        gameplayUI.SetActive(true);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
        startButton.SetActive(true);
        stopButton.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        // only allow H toggle during Building
        if (GameManager.Instance.CurrentState == GameManager.GameState.Building
            && Input.GetKeyDown(KeyCode.H))
        {
            ToggleUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ToggleUI()
    {
        uiHidden = !uiHidden;
        gameplayUI.SetActive(!uiHidden);
    }

    public void ShowWinScreen()
    {
        winPanel.SetActive(true);
        stopButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void OnGameStarted()
    {
        gameplayUI.SetActive(false);
        startButton.SetActive(false);
        stopButton.SetActive(true);
    }

    public void OnGameStopped()
    {
        gameplayUI.SetActive(true);
        startButton.SetActive(true);
        stopButton.SetActive(false);
        if (isPaused) TogglePause();
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("LevelSelect");
    }
}
