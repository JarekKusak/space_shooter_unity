using TMPro; // Pro TextMeshPro
using UnityEngine;

public class GameTimer : MonoBehaviour {
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
}