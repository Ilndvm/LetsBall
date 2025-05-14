using UnityEngine;

public class MoverEnemy : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public Sprite sprite1;
    public Sprite sprite2;
    public float animationSpeed = 0.3f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Vector3 target;
    private float animTimer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        target = pointB.position;
    }

    void Update()
    {
        Vector3 dir = (target - transform.position).normalized;
        rb.linearVelocity = dir * speed;

        if (Vector2.Distance(transform.position, target) < 0.1f)
        {
            target = (target == pointA.position) ? pointB.position : pointA.position;
        }

        AnimateSprite();
    }

    void AnimateSprite()
    {
        animTimer += Time.deltaTime;
        if (animTimer >= animationSpeed)
        {
            sr.sprite = sr.sprite == sprite1 ? sprite2 : sprite1;
            animTimer = 0f;
        }
    }
}
