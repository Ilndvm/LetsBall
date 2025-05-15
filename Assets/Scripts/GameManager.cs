using UnityEngine;
using UnityEngine.SceneManagement;

// Controls the game state: Building vs Playing, ball spawning, win/stop logic
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Building, Playing }
    public GameState CurrentState = GameState.Building;

    [Header("Gameplay")]
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;
    public Transform padsParent; // Parent object holding all pads
    public GameObject evilBallPrefab;
    public Transform evilBallSpawnPoint;

    GameObject currentBall;
    GameObject evilBall;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    // Called by Start button
    public void StartGame()
    {
        if (CurrentState == GameState.Playing) return;

        CurrentState = GameState.Playing;
        currentBall = Instantiate(ballPrefab, ballSpawnPoint.position, Quaternion.identity);

        if (evilBallPrefab != null)
        {
            evilBall = Instantiate(evilBallPrefab, evilBallSpawnPoint.position, Quaternion.identity);
        }

        // disable building UI/controls
        BuildManager.Instance.DisableBuilding();
        UIManager.Instance.OnGameStarted();
    }

    // Called by Stop button
    public void StopGame()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Building;

        GameObject[] allBalls = GameObject.FindGameObjectsWithTag("Ball");
        foreach (GameObject ball in allBalls)
        {
            Destroy(ball);
        }

        // re-enable building UI/controls
        BuildManager.Instance.EnableBuilding();
        UIManager.Instance.OnGameStopped();
        ResetScripts();
    }

    // Called when the ball reaches the goal
    public void Win()
    {
        CurrentState = GameState.Building;

        if (ProgressManager.Instance != null)
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            ProgressManager.Instance.UnlockNextLevel(levelIndex);
        }

        UIManager.Instance.ShowWinScreen();
    }

    public void ResetScripts()
    {
        if (padsParent == null)
        {
            Debug.LogWarning("Pads parent not assigned in GameManager.");
            return;
        }

        foreach (Transform child in padsParent)
        {
            SplitterPad splitter = child.GetComponent<SplitterPad>();
            if (splitter != null) splitter.Reset();

            GrowPad growPad = child.GetComponent<GrowPad>();
            if (growPad != null) growPad.Reset();

            ShrinkPad shrinkPad = child.GetComponent<ShrinkPad>();
            if (shrinkPad != null) shrinkPad.Reset();

            // Add more pad types here if needed
        }
    }
}
