using UnityEngine;

public class ScreenSwitch : MonoBehaviour
{
    public GameObject upperCharacter; // Üst ekrandaki karakter
    public GameObject lowerCharacter; // Alt ekrandaki karakter
    public Vector3 lowerCharacterOffset; // Alt karakterin pozisyonu için offset (üst karaktere göre farklı parkurda)

    private bool isOnUpperScreen = true; // Karakter başlangıçta üst ekranda

    void Update()
    {
        // Space tuşuna basıldığında ekranlar arasında geçiş yapılıyor
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchScreen();
        }
    }

    void SwitchScreen()
    {
        if (isOnUpperScreen)
        {
            // Karakteri üst ekrandan alt ekrana geçiriyoruz
            upperCharacter.SetActive(false); // Üst ekran karakterini devre dışı bırak
            lowerCharacter.SetActive(true);  // Alt ekran karakterini aktif hale getir
            // Alt karakteri üst karakterin pozisyonuna göre farklı bir parkura taşı
            lowerCharacter.transform.position = upperCharacter.transform.position + lowerCharacterOffset;
        }
        else
        {
            // Karakteri alt ekrandan üst ekrana geçiriyoruz
            upperCharacter.SetActive(true);
            lowerCharacter.SetActive(false);
            // Üst karakteri alt karakterin pozisyonuna göre taşı
            upperCharacter.transform.position = lowerCharacter.transform.position - lowerCharacterOffset;
        }

        isOnUpperScreen = !isOnUpperScreen; // Durumu tersine çeviriyoruz
    }
}
