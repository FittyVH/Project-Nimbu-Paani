using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    bool menuOpen = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] InGameUI inGameUI;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && inGameUI.gameStarted)
        {
            menuOpen = !menuOpen;
        }

        if (menuOpen)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!menuOpen && inGameUI.gameStarted)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }

    // button actions
    public void OnResumeClicked()
    {
        menuOpen = false;
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuClicked()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
