using TMPro;
using UnityEngine;
using UnityEngine.UI; // Pro použití UI komponent

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    public TextMeshPro healthText;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private int aliensKilled = 0;

    public string GetAliensKilled()
    {
        return aliensKilled.ToString();
    }
    
    public string GetScore()
    {
        return score.ToString();
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

    public void AlienKilled(bool isAdvanced) {
        aliensKilled++;
        score += isAdvanced ? 100 : 50;  // například 100 bodů za advanced, 50 za basic
        scoreText.text = $"Aliens killed: {aliensKilled}\nScore: {score}";
    }
}