using UnityEngine;

public class KillPad : MonoBehaviour
{
    public ParticleSystem deathParticle;
    public ParticleSystem evilDeathParticle;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.StartsWith("EvilBall"))
        {
            Destroy(collision.gameObject);
            Debug.Log("EvilBall destroyed by KillPad!");

            Instantiate(evilDeathParticle, collision.transform.position, Quaternion.identity);

            AudioManager.Instance.PlaySound(AudioManager.Sound.KillPad);
        }
        else if (collision.gameObject.name.StartsWith("Ball"))
        {
            Destroy(collision.gameObject);
            Debug.Log("Ball destroyed by KillPad!");

            Instantiate(deathParticle, collision.transform.position, Quaternion.identity);

            AudioManager.Instance.PlaySound(AudioManager.Sound.KillPad);
        }
        else if (collision.gameObject.name.StartsWith("BackgroundBall"))
        {
            Destroy(collision.gameObject);
            Debug.Log("BackgroundBall destroyed by KillPad!");

            Instantiate(deathParticle, collision.transform.position, Quaternion.identity);
        }
    }
}
