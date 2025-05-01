using UnityEngine;

public class GrowPad : MonoBehaviour
{
    public float growFactor = 1.5f;  // Scale multiplier
    public bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canTrigger)
        {
            collision.transform.localScale *= growFactor;

            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.mass *= growFactor;
            }
            canTrigger = false;
        }
    }

    public void Reset()
    {
        canTrigger = true;
    }
}
