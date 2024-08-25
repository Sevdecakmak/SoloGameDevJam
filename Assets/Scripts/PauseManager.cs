using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;  // Pause menü paneli (UI'dan ayarlayacağız)
    private bool isPaused = false;  // Oyun duraklatıldı mı kontrolü

    private void Start()
    {
       pauseMenuUI.SetActive(false); 
    }

    void Update()
    {
        // Eğer Esc tuşuna basılırsa
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();  // Oyun devam etsin
            }
            else
            {
                Pause();   // Oyun dursun
            }
        }
    }

    // Oyunu devam ettir
    public void Resume()
    {
        pauseMenuUI.SetActive(false);  // Pause menüyü gizle
        Time.timeScale = 1f;  // Oyunu normale döndür (1x hız)
        isPaused = false;     // Duraklatma modundan çık
    }

    // Oyunu durdur
    void Pause()
    {
        pauseMenuUI.SetActive(true);   // Pause menüyü göster
        Time.timeScale = 0f;  // Oyun zamanını durdur (0x hız)
        isPaused = true;      // Duraklatma moduna geç
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;  // Oyun zamanı sıfırlanarak ana menüye dönülür
        SceneManager.LoadScene(0);  // Ana menü sahnesini yükle
    }

    // Oyunu yeniden başlatmak
    public void RestartGame()
    {
        Time.timeScale = 1f;  // Oyun zamanı sıfırlanır
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  // Şu anki sahneyi yeniden yükle
    }
}
