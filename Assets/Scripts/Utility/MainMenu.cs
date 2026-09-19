using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject controlsPanel;
    public GameObject levelSelectPanel;

    [Header("Scene Names")]
    public string level1Scene = "Level1";
    public string level2Scene = "Level2";

    void Start()
    {
        ShowMainMenu(); // show main menu at start
    }

    // ==============================
    // MENU NAVIGATION
    // ==============================
    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        controlsPanel.SetActive(false);
        levelSelectPanel.SetActive(false);
    }

    public void ShowControls()
    {
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void ShowLevelSelect()
    {
        mainMenuPanel.SetActive(false);
        levelSelectPanel.SetActive(true);
    }

    // ==============================
    // BUTTON FUNCTIONS
    // ==============================
    public void PlayGame()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(level1Scene);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(level2Scene);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game!");
        Application.Quit();
    }
}
