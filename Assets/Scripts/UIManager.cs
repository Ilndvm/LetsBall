using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("UI References")]
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject startButton;
    [SerializeField] private GameObject stopButton;
    [SerializeField] private ScrollRect blockSelectionPanel;

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

        if (blockSelectionPanel != null)
        {
            blockSelectionPanel.verticalNormalizedPosition = 1f;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

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

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextIndex);
        }
        else
        {
            Debug.LogWarning("No more levels! Reloading current level.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");  // make sure the name matches your main menu scene
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
        gameplayUI.SetActive(!isPaused);
        startButton.SetActive(!isPaused);
        stopButton.SetActive(!isPaused);
        pausePanel.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;
    }
}
