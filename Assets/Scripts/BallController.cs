using UnityEngine;

public class BallController : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FinishPoint"))
        {
            // Stop movement
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            // Option 1: Freeze completely (still responds to physics)
            // rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // Option 2: Make static (no more physics)
            rb.bodyType = RigidbodyType2D.Static;

            GameManager.Instance.Win();
        }
    }
}
