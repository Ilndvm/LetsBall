using UnityEngine;

public class ShrinkPad : MonoBehaviour
{
    public float shrinkFactor = 0.5f;  // Scale multiplier (e.g., 0.5 = half size)
    public bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canTrigger)
        {
            collision.transform.localScale *= shrinkFactor;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.mass *= shrinkFactor;
            }
            canTrigger = false;
        }
    }

    public void Reset()
    {
        canTrigger = true;
    }
}
