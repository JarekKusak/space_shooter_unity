using TMPro;
using UnityEngine;
using UnityEngine.UI; // Pro použití UI komponent

public class UIManager : MonoBehaviour
{
    public TextMeshPro healthText;
    public TextMeshPro scoreText;

    private int score = 0;
    private int aliensKilled = 0;

    public void UpdateHealth(int currentHealth) {
        healthText.text = "[" + new string('|', currentHealth) + "]";
    }

    public void AlienKilled(bool isAdvanced) {
        aliensKilled++;
        score += isAdvanced ? 100 : 50;  // například 100 bodů za advanced, 50 za basic
        scoreText.text = $"Aliens killed: {aliensKilled}\nScore: {score}";
    }
}