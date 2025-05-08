using UnityEngine;

public class BackgroundBallSpawner : MonoBehaviour
{
    public GameObject ballPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 2f;

    private GameObject currentBall;

    void Start()
    {
        SpawnBall();
    }

    void SpawnBall()
    {
        currentBall = Instantiate(ballPrefab, spawnPoint.position, Quaternion.identity);

        BallDestroyHandler handler = currentBall.AddComponent<BallDestroyHandler>();
        handler.spawner = this;
    }

    public void OnBallDestroyed()
    {
        Invoke(nameof(SpawnBall), respawnDelay);
    }
}
