using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipCutscene : MonoBehaviour
{
    [SerializeField] DialogueSystem dialogueSystem;
    [SerializeField] GameObject pauseMenu;
    bool pauseMenuOpen;

    void Start()
    {
        // pauseMenuOpen = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuOpen = !pauseMenuOpen;
        }

        if (pauseMenuOpen)
        {
            pauseMenu.SetActive(true);
            dialogueSystem.enabled = false;
        }
        if (!pauseMenuOpen)
        {
            pauseMenu.SetActive(false);
            dialogueSystem.enabled = true;
        }
    }

    public void OnResumeClicked()
    {
        pauseMenuOpen = false;
    }

    public void OnSkipClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OnRestartClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnMainMenuClicked()
    {
        pauseMenuOpen = false;
        SceneManager.LoadScene("MainMenu");
    }
}
