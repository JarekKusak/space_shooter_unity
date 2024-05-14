using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI resumeText;
    public TextMeshProUGUI mainMenuText;
    private int selectedIndex = 0;
    private Color highlightedColor = Color.red;
    private Color defaultColor = Color.white;

    void Start()
    {
        UpdateMenu();
        gameObject.SetActive(false);  // Initially hide the pause menu
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("čauky mňauky");
            if (!gameObject.activeInHierarchy)
            {
                Time.timeScale = 0; // Pause the game
                gameObject.SetActive(true);  // Show the pause menu
                UpdateMenu();
            }
            else
            {
                ContinueGame(); // Resume the game
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            selectedIndex = (selectedIndex - 1 + 2) % 2;
            UpdateMenu();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            selectedIndex = (selectedIndex + 1) % 2;
            UpdateMenu();
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.Space))
        {
            if (selectedIndex == 0)
                ContinueGame();
            else if (selectedIndex == 1)
            {
                Time.timeScale = 1; // Resume time before loading scene
                SceneManager.LoadScene("MainMenu");
            }
        }
    }

    void UpdateMenu()
    {
        resumeText.color = (selectedIndex == 0) ? highlightedColor : defaultColor;
        mainMenuText.color = (selectedIndex == 1) ? highlightedColor : defaultColor;
    }

    private void ContinueGame()
    {
        Time.timeScale = 1; // Resume the game
        gameObject.SetActive(false);  // Hide the pause menu
    }
}