using UnityEngine;

public class JumpPad2D : MonoBehaviour
{
    public float jumpForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 jumpDirection = Vector2.up;
                rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            }
        }
    }
}
