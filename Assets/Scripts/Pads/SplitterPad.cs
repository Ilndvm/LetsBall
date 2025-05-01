using UnityEngine;

public class SplitterPad : MonoBehaviour
{
    public GameObject ballPrefab;
    public float splitSpeed = 10f;
    public float newScale = 0.5f; // Optional: make split balls smaller
    public bool canTrigger = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && canTrigger)
        {
            Vector3 position = this.transform.position;
            Destroy(collision.gameObject);

            // Create two new balls
            CreateSplitBall(position + new Vector3(1, 0, 0), new Vector2(1, 1).normalized);
            CreateSplitBall(position + new Vector3(-1, 0, 0), new Vector2(-1, 1).normalized);
            canTrigger = false;
        }
    }

    void CreateSplitBall(Vector3 position, Vector2 direction)
    {
        GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
        newBall.transform.localScale *= newScale;
        Rigidbody2D rb = newBall.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = direction * splitSpeed;
            rb.mass *= newScale;
        }
    }

    public void Reset()
    {
        canTrigger = true;
    }
}
