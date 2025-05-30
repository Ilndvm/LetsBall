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

            //Grey out Pad because it is one time user
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = 0.5f;
                spriteRenderer.color = newColor;
            }
            canTrigger = false;

            AudioManager.Instance.PlaySound(AudioManager.Sound.GrowPad);
        }
    }

    public void Reset()
    {
        //Reset color
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            Color newColor = spriteRenderer.color;
            newColor.a = 1f;
            spriteRenderer.color = newColor;
        }

        canTrigger = true;
    }
}
