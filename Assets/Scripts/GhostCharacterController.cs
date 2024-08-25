using UnityEngine;
using System.Collections;

public class GhostCharacterController : MonoBehaviour
{
    public bool canFollow = true;
    private Animator ghostAnimator;  // Animator bileşeni için referans

    private void Start()
    {
        ghostAnimator = GetComponent<Animator>(); // Animator bileşenini al
    }

    public void StopFollowing()
    {
        canFollow = false;
        Debug.Log("Ghost takip etmeyi bıraktı.");
    }

    public void StartFollowing()
    {
        canFollow = true;
    }

    // Animasyonu 1 saniye oynat ve sonra bitir
    public void PlayTeleportAnimation(float duration)
    {
        if (ghostAnimator != null)
        {
            StartCoroutine(TeleportAnimationCoroutine(duration));
        }
    }

    private IEnumerator TeleportAnimationCoroutine(float duration)
    {
        // Animasyonu başlat
        ghostAnimator.SetBool("isTeleporting", true);

        // Belirtilen süre boyunca bekle
        yield return new WaitForSeconds(duration);

        // Animasyonu durdur
        ghostAnimator.SetBool("isTeleporting", false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Ghost collided with an obstacle!");
            StopFollowing();
        }
    }
}
