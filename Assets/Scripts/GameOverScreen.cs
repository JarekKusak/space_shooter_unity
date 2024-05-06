using System.Collections;
using TMPro; // Pro TextMeshPro
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOverScreen : MonoBehaviour {
    public GameObject gameOverPanel; // Odkaz na panel koncové obrazovky
    public TextMeshProUGUI gameOverText; // Hlavní nápis "GAME OVER"
    public TextMeshProUGUI timeText; // Text pro zobrazení času
    public TextMeshProUGUI aliensKilledText; // Text pro zobrazení počtu zabitých alienů
    public TextMeshProUGUI scoreText; // Text pro zobrazení skóre

    private UIManager uiManager;
    private GameTimer gameTimer;

    void Start() {
        gameOverPanel.SetActive(false); // Panel koncové obrazovky na začátku skryjeme
        uiManager = FindObjectOfType<UIManager>();
        gameTimer = FindObjectOfType<GameTimer>();
    }

    public void ShowGameOverScreen() {
        // Zmrazíme hru
        Time.timeScale = 0;

        // Připravíme data pro zobrazení
        gameOverText.text = "GAME OVER";
        timeText.text = "Time: " + gameTimer.GetElapsedTime();
        aliensKilledText.text = "Aliens killed: " + uiManager.GetAliensKilled();
        scoreText.text = "Score: " + uiManager.GetScore();

        // Zobrazíme panel koncové obrazovky
        gameOverPanel.SetActive(true);
        StartCoroutine(ReturnToMainMenuAfterDelay(2f));
    }
    
    private IEnumerator ReturnToMainMenuAfterDelay(float delay) {
        yield return new WaitForSecondsRealtime(delay); // Použití reálného času
        Time.timeScale = 1; // Obnovení časové škály
        SceneManager.LoadScene("MainMenu"); // Nahraďte názvem scény vašeho hlavního menu
    }

    // Metoda pro obnovení hry
    public void ResetGame() {
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
    }
}