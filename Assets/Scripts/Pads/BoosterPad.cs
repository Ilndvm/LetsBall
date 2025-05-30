using UnityEngine;

public class BoosterPad2D : MonoBehaviour
{
    public Vector2 boostDirection = Vector2.right; // Right direction by default
    public float boostForce = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.AddForce(boostDirection.normalized * boostForce, ForceMode2D.Impulse);
            }

            AudioManager.Instance.PlaySound(AudioManager.Sound.BoosterPad);
        }
    }
}
