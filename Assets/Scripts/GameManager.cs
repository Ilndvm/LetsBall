using UnityEngine;

// Controls the game state: Building vs Playing, ball spawning, win/stop logic
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public enum GameState { Building, Playing }
    public GameState CurrentState = GameState.Building;

    [Header("Gameplay")]
    public GameObject ballPrefab;
    public Transform ballSpawnPoint;

    GameObject currentBall;

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

        // disable building UI/controls
        BuildManager.Instance.DisableBuilding();
        UIManager.Instance.OnGameStarted();
    }

    // Called by Stop button
    public void StopGame()
    {
        if (CurrentState != GameState.Playing) return;

        CurrentState = GameState.Building;
        if (currentBall != null) Destroy(currentBall);

        // re-enable building UI/controls
        BuildManager.Instance.EnableBuilding();
        UIManager.Instance.OnGameStopped();
    }

    // Called when the ball reaches the goal
    public void Win()
    {
        CurrentState = GameState.Building;
        UIManager.Instance.ShowWinScreen();
        Debug.Log("YOU WIN!");
    }
}
