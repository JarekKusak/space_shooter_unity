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
    public void UpdateHealth(int currentHealth) {
        healthText.text = "[" + new string('|', currentHealth) + "]";
    }

    public void AlienKilled(bool isAdvanced) {
        aliensKilled++;
        score += isAdvanced ? 100 : 50;  // například 100 bodů za advanced, 50 za basic
        scoreText.text = $"Aliens killed: {aliensKilled}\nScore: {score}";
    }
}