using TMPro;
using UnityEngine;
using UnityEngine.UI; // Pro použití UI komponent

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshPro healthText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI ammoText; // UI element pro zobrazení střel
    public TextMeshProUGUI shieldPointsText; // UI element pro zobrazení štítových bodů
    
    private int score = 0;
    private int aliensKilled = 0;
    private int shieldPoints = 0; // Počet štítových bodů

    public TextMeshProUGUI timerText; // Odkaz na UI element pro zobrazení času
    private float elapsedTime; // Uchovává čas od spuštění hry

    public string GetElapsedTime()
    {
        return elapsedTime.ToString();
    }
    void Update() {
        // Zvýšíme uběhnutý čas o časový interval mezi jednotlivými snímky
        elapsedTime += Time.deltaTime;

        // Převod uběhnutého času na minuty a sekundy
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);

        // Aktualizace textu, aby zobrazoval formátovaný čas
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public string GetAliensKilled()
    {
        return aliensKilled.ToString();
    }
    
    public string GetScore()
    {
        return score.ToString();
    }
    
    public void UpdateAmmo(int currentAmmo, int maxAmmo) {
        ammoText.text = "[" + new string('+', currentAmmo).PadRight(maxAmmo, ' ') + "]";
    }
    
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        // Počet bloků, které se zobrazí
        int totalBlocks = 10;
        // Vypočítáme poměrně, kolik bloků by mělo být zobrazeno
        int filledBlocks = Mathf.RoundToInt(((float)currentHealth / maxHealth) * totalBlocks);

        // Zajistíme, že se vždy zobrazí alespoň jeden blok, pokud hráč ještě není mrtvý
        if (filledBlocks == 0 && currentHealth > 0)
        {
            filledBlocks = 1;
        }

        // Vytvoříme začátek řetězce
        string healthBar = "[";
    
        // Přidáme bloky odpovídající současnému zdraví
        for (int i = 0; i < filledBlocks; i++)
        {
            healthBar += "#";
        }
        healthBar = healthBar.PadRight(totalBlocks + 1, ' ');
        healthBar += "]";
        healthText.text = healthBar;
    }
    
    public void UpdateShieldPoints(int points) {
        shieldPoints = points;
        shieldPointsText.text = "[" + new string('0', points).PadRight(3, ' ') + "]";
    }

    public void AlienKilled(bool isAdvanced) {
        aliensKilled++;
        score += isAdvanced ? 100 : 50;  // například 100 bodů za advanced, 50 za basic
        scoreText.text = $"Aliens killed: {aliensKilled}\nScore: {score}";
    }
}