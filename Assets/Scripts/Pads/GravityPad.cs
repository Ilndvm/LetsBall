using UnityEngine;

public class GravityPad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale *= -1f;
            }
            AudioManager.Instance.PlaySound(AudioManager.Sound.GravityPad, 0.5f);
        }
    }
}
