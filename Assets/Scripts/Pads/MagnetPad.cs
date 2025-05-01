using UnityEngine;

public class MagnetPad : MonoBehaviour
{
    public float pullForce = 10f; // strength of attraction

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = (transform.position - collision.transform.position).normalized;
                rb.AddForce(direction * pullForce);
            }
        }
    }
}
