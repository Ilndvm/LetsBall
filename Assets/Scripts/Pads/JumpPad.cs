using UnityEngine;

public class JumpPad2D : MonoBehaviour
{
    public float jumpForce = 10f;
    public Sprite activatedSprite;
    private Sprite originalSprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalSprite = spriteRenderer.sprite;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (spriteRenderer != null && activatedSprite != null)
                spriteRenderer.sprite = activatedSprite;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 jumpDirection = Vector2.up;
                rb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            if (spriteRenderer != null)
                spriteRenderer.sprite = originalSprite;
        }
    }
}
