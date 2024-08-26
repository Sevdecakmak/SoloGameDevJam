using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterController2D : MonoBehaviour
{
    [SerializeField]
    public float moveSpeed = 5f;
    [SerializeField]
    public float jumpForce = 10f;

    float moveX;

    public Transform mainCharacter;
    public Transform ghostCharacter;
    public Transform upperParkour;
    public Transform lowerParkour;

    public bool isOnUpperParkour = true;

    private Rigidbody2D mainRb;
    private Animator anim;

    private bool isJumping;

    private GhostCharacterController ghostController;

    void Start()
    {
        mainRb = mainCharacter.GetComponent<Rigidbody2D>();
        anim = mainCharacter.GetComponent<Animator>(); // Animator bileşenini alıyoruz
        ghostController = ghostCharacter.GetComponent<GhostCharacterController>();
    }

    void Update()
    {
        // Hareket için yatay input al
        moveX = Input.GetAxisRaw("Horizontal");

        // Animasyonları güncelle
        anim.SetFloat("Speed", Mathf.Abs(moveX)); // Hızına göre yürüme animasyonu

        // Zıplama input'u
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }

        // Zıplama durumunu animatorda güncelle
        anim.SetBool("isJumping", isJumping);

        // Parkur değiştirme
        if (Input.GetKeyDown(KeyCode.K) && ghostController.canFollow)
        {
            ToggleParkour();
        }
    }

    void FixedUpdate()
    {
        // Ana karakteri hareket ettir
        Movement();

        // Ghost'un takibi, ghost karakterin canFollow değeri true ise devam eder
        if (ghostController.canFollow)
        {
            if (isOnUpperParkour)
            {
                ghostCharacter.position = new Vector2(mainCharacter.position.x, lowerParkour.position.y);  // Ghost alt parkurda
            }
            else
            {
                ghostCharacter.position = new Vector2(mainCharacter.position.x, upperParkour.position.y);  // Ghost üst parkurda
            }
        }
    }

    void Movement()
    {
        // Hareket etmek için velocity kullan
        mainRb.velocity = new Vector2(moveX * moveSpeed, mainRb.velocity.y);
    }

    public void TeleportGhostParallelPosition()
    {
        if (isOnUpperParkour)
        {
            ghostCharacter.position = new Vector2(mainCharacter.position.x, lowerParkour.position.y);
        }
        else
        {
            ghostCharacter.position = new Vector2(mainCharacter.position.x, upperParkour.position.y);
        }

        ghostController.PlayTeleportAnimation(0.5f);
        ghostController.StartFollowing();
    }

    // Parkur değiştirme fonksiyonu
    void ToggleParkour()
    {
        isOnUpperParkour = !isOnUpperParkour;

        if (isOnUpperParkour)
        {
            mainCharacter.position = new Vector2(mainCharacter.position.x, upperParkour.position.y);
        }
        else
        {
            mainCharacter.position = new Vector2(mainCharacter.position.x, lowerParkour.position.y);
        }
    }

    // Zıplama fonksiyonu
    void Jump()
    {
        if (!isJumping)
        {
            mainRb.velocity = new Vector2(mainRb.velocity.x, jumpForce);  // Yalnızca zıplama kuvvetini uygula
            isJumping = true;
            anim.SetTrigger("Jump");  // Zıplama animasyonunu tetikle
            Debug.Log("Jumping!");
        }
    }

    // Yere temas kontrolü
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Platform"))
        {
            isJumping = false;  // Karakter yere indi
        }
    }

    // Trigger alanına giriş kontrolü
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("MergeTrigger")) // Trigger alanının tag'ı "MergeTrigger" olarak ayarlanmış olsun
        {
            // Ghost'u ana karakterle birleştir
            ghostCharacter.position = mainCharacter.position;

            ghostController.canFollow = false;

        }

        if (other.CompareTag("GameOver"))
        {
            SceneManager.LoadScene(2);
        }

        if (other.CompareTag("Finish"))
        {
            SceneManager.LoadScene(3);
        }
    }
}