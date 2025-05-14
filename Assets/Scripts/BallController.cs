using UnityEngine;

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

            GameManager.Instance.Win();
        }
    }
}
