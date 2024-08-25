using UnityEngine;

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

    private bool isGrounded;

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
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }

        // Yerde olup olmadığını animatorda ayarlamak için
        anim.SetBool("isGrounded", isGrounded);

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

    public void TeleportGhostParallerlPosition()
    {
        if (isOnUpperParkour)
        {
            ghostCharacter.position = new Vector2(mainCharacter.position.x, lowerParkour.position.y);
        }
        else
        {
            ghostCharacter.position = new Vector2(mainCharacter.position.x, upperParkour.position.y);
        }

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
        if (isGrounded)
        {
            mainRb.velocity = new Vector2(mainRb.velocity.x, jumpForce);  // Yalnızca zıplama kuvvetini uygula
            isGrounded = false;
            anim.SetTrigger("Jump");  // Zıplama animasyonunu tetikle
            Debug.Log("Jumping!");
        }
    }

    // Yere temas kontrolü
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // Karakterin yere temas ettiğini belirt
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;  // Karakterin yerden ayrıldığını belirt
        }
    }
}
