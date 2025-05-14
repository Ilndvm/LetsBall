using UnityEngine;

public class JumperEnemy : MonoBehaviour
{
    public float jumpForce = 10f;
    public float jumpDelay = 2f;

    public Sprite groundedSprite;
    public Sprite airborneSprite;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private float jumpTimer;

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        isGrounded = true;
        sr.sprite = groundedSprite;
    }

    void Update()
    {
        if (isGrounded)
        {
            jumpTimer += Time.deltaTime;
            if (jumpTimer >= jumpDelay)
            {
                Jump();
                jumpTimer = 0f;
            }
        }

        UpdateSprite();
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f); // reset vertical velocity
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    void UpdateSprite()
    {
        sr.sprite = isGrounded ? groundedSprite : airborneSprite;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Basic ground check
        if (collision.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
        }
    }
}
