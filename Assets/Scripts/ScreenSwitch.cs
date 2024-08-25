using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    public GameObject upperCharacter; // Üst ekrandaki karakter
    public GameObject lowerCharacter; // Alt ekrandaki karakter
    public float transparency = 0.5f; // Silüet için transparanlık değeri

    private bool isOnUpperScreen = true; // Karakterin başlangıçta üst ekranda olduğunu varsayıyoruz

    private SpriteRenderer upperSpriteRenderer;
    private SpriteRenderer lowerSpriteRenderer;

    void Start()
    {
        // Üst ve alt karakterin SpriteRenderer bileşenlerini alıyoruz
        upperSpriteRenderer = upperCharacter.GetComponent<SpriteRenderer>();
        lowerSpriteRenderer = lowerCharacter.GetComponent<SpriteRenderer>();

        // Başlangıçta sadece üst karakter normal görünür, alt karakter silüet olur
        SetCharacterVisibility();
    }

    void Update()
    {
        // Karakterin hareketi ve ekranlar arası geçiş için input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchScreen();
        }
    }

    void SwitchScreen()
    {
        // Geçiş yapılıyor, hangi ekranda olduğunu tersine çeviriyoruz
        isOnUpperScreen = !isOnUpperScreen;

        // Görünürlük ayarlarını güncelliyoruz
        SetCharacterVisibility();
    }

    void SetCharacterVisibility()
    {
        if (isOnUpperScreen)
        {
            // Üst karakter normal, alt karakter silüet olur
            SetAlpha(upperSpriteRenderer, 1f);  // Üst karakter tam görünür
            SetAlpha(lowerSpriteRenderer, transparency);  // Alt karakter transparan
        }
        else
        {
            // Alt karakter normal, üst karakter silüet olur
            SetAlpha(upperSpriteRenderer, transparency);  // Üst karakter transparan
            SetAlpha(lowerSpriteRenderer, 1f);  // Alt karakter tam görünür
        }
    }

    void SetAlpha(SpriteRenderer spriteRenderer, float alpha)
    {
        Color color = spriteRenderer.color;
        color.a = alpha;
        spriteRenderer.color = color;
    }
}
