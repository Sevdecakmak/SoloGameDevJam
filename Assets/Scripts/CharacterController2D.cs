using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform shadowCharacter; // Alt ekrandaki silüet karakter
    public Vector3 shadowOffset; // Silüet karakterin farklı bir pozisyonda olabilmesi için offset

    private Vector2 movement;

    void Update()
    {
        // Karakter hareketi için inputları alıyoruz.
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Üst ekrandaki karakteri hareket ettiriyoruz.
        transform.Translate(movement * moveSpeed * Time.deltaTime);

        // Silüet karakterin hareket yönünü üst karakterle eşleştiriyoruz, ancak pozisyon farklı olabilir
        if (shadowCharacter != null)
        {
            shadowCharacter.Translate(movement * moveSpeed * Time.deltaTime);
        }
    }
}
