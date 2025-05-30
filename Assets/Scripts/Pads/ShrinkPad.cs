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

            //Grey out Pad because it is one time user
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = 0.5f;
                spriteRenderer.color = newColor;
            }
            canTrigger = false;

            AudioManager.Instance.PlaySound(AudioManager.Sound.ShrinkPad);
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
