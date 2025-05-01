using UnityEngine;

public class KillPad : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Ball destroyed by KillPad!");
        }
    }
}
