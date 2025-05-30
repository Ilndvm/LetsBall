using UnityEngine;

public class SplitterPad : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject ballPrefab;
    public GameObject evilBallPrefab;

    [Header("Split Settings")]
    public float splitSpeed = 10f;
    public float newScale = 0.5f;
    private Transform gameobjectTransform;

    bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!canTrigger) return;

        if (collision.gameObject.name.StartsWith("Ball") || collision.gameObject.name.StartsWith("EvilBall"))
        {
            GameObject prefabToUse = collision.gameObject.name.StartsWith("EvilBall")
                ? evilBallPrefab
                : ballPrefab;

            gameobjectTransform = collision.gameObject.transform;

            Vector3 spawnPos = transform.position;

            Destroy(collision.gameObject);

            CreateSplitBall(prefabToUse, spawnPos + Vector3.right, new Vector2(1, 1).normalized);
            CreateSplitBall(prefabToUse, spawnPos + Vector3.left, new Vector2(-1, 1).normalized);

            //Grey out Pad because it is one time user
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                Color newColor = spriteRenderer.color;
                newColor.a = 0.5f;
                spriteRenderer.color = newColor;
            }

            canTrigger = false;

            AudioManager.Instance.PlaySound(AudioManager.Sound.SplitterPad);
        }
    }

    private void CreateSplitBall(GameObject prefab, Vector3 position, Vector2 direction)
    {
        GameObject newBall = Instantiate(prefab, position, Quaternion.identity);
        newBall.transform.localScale = gameobjectTransform.localScale * newScale;

        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * splitSpeed;
            rb.mass *= newScale;
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
