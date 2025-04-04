using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InGameUI inGameUI;

    // button actions
    public void OnResumeClicked()
    {
        inGameUI.pauseMenuOpen = false;
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
