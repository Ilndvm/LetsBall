using UnityEngine;

public class BallDestroyHandler : MonoBehaviour
{
    public BackgroundBallSpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
        {
            spawner.OnBallDestroyed();
        }
    }
}
