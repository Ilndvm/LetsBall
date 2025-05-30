using UnityEngine;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{
    [Header("Initial Movement")]
    public float initialPushForce = 1f;

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // give it a little nudge to the right
        rb.AddForce(Vector2.right * initialPushForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishPoint"))
        {
            // stop all motion
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // make completely static so it can't move or rotate
            rb.bodyType = RigidbodyType2D.Static;

            AudioManager.Instance.PlaySound(AudioManager.Sound.FinishPoint);
            GameManager.Instance.Win();
        }

        if (collision.CompareTag("Star"))
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            if (ProgressManager.Instance != null)
            {
                int prev = ProgressManager.Instance.GetStars(levelIndex);
                ProgressManager.Instance.SaveStars(levelIndex, Mathf.Min(prev + 1, 3));
            }

            AudioManager.Instance.PlaySound(AudioManager.Sound.Collectible);

            Destroy(collision.gameObject);
        }
    }
}
