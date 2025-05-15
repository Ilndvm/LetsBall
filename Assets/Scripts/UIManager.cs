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
    bool _wasPlaying = false;  // remembers what state we were in before pausing

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
            blockSelectionPanel.verticalNormalizedPosition = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartLevel();

        if (GameManager.Instance.CurrentState == GameManager.GameState.Building
            && Input.GetKeyDown(KeyCode.H) && !winPanel.activeSelf)
        {
            ToggleUI();
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !winPanel.activeSelf)
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
            SceneManager.LoadScene(nextIndex);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void ToggleUI()
    {
        TooltipManager.Instance.HideTooltip();
        uiHidden = !uiHidden;
        startButton.SetActive(!uiHidden);
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
        if (isPaused)
            TogglePause();  // ensure pause UI clears if we stopped while paused
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            // entering pause: record if we were playing
            _wasPlaying = (GameManager.Instance.CurrentState == GameManager.GameState.Playing);
        }

        // show/hide the pause panel
        pausePanel.SetActive(isPaused);

        if (isPaused)
        {
            // hide gameplay UI while paused
            gameplayUI.SetActive(false);
            startButton.SetActive(false);
            stopButton.SetActive(false);
            Time.timeScale = 0f;
        }
        else
        {
            // unpausing: restore UI based on pre-pause game state
            Time.timeScale = 1f;
            if (_wasPlaying)
            {
                // We were in play mode: show stop button
                gameplayUI.SetActive(false);
                startButton.SetActive(false);
                stopButton.SetActive(true);
            }
            else
            {
                // We were in build mode: show start button
                gameplayUI.SetActive(true);
                startButton.SetActive(true);
                stopButton.SetActive(false);
            }
        }
    }
}
