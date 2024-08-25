using UnityEngine;

public class GhostCharacterController : MonoBehaviour
{
    public bool canFollow = true; 

    public void StopFollowing()
    {
        canFollow = false; 
        Debug.Log("Ghost takip etmeyi bıraktı.");
    }

    public void StartFollowing()
    {
        canFollow = true;
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
