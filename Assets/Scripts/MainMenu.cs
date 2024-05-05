using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI playText;
    public TextMeshProUGUI exitText;
    private int selectedIndex = 0;
    private Color highlightedColor = Color.red;
    private Color defaultColor = Color.white;

    void Start()
    {
        UpdateMenu();
    }

    void Update()
    {
        // Navigace v menu
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + 2) % 2; // Nahoru
            UpdateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % 2; // Dolů
            UpdateMenu();
        }

        // Potvrzení položky
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedIndex == 0)
                SceneManager.LoadScene("SampleScene"); // Změňte na název herní scény
            else if (selectedIndex == 1)
                Application.Quit(); // Ukončí aplikaci
        }
    }

    void UpdateMenu()
    {
        playText.color = (selectedIndex == 0) ? highlightedColor : defaultColor;
        exitText.color = (selectedIndex == 1) ? highlightedColor : defaultColor;
    }
}