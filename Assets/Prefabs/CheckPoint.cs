using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("Ball"))
        {
            GameManager.Instance.ballSpawnPoint = this.GetComponent<Transform>();
        }
    }
}
