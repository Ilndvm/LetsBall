using UnityEngine;

public class BallController : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishPoint"))
        {
            GameManager.Instance.Win();
            Destroy(gameObject);
        }
    }
}
