using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameUI : MonoBehaviour
{
    [SerializeField] GameObject startIngredientUI;
    [SerializeField] GameObject inGameIngredientList;
    [SerializeField] GameObject serveDrinkPromptUI;
    [SerializeField] GameObject canvas;

    //pause menu 
    public bool pauseMenuOpen = false;

    [SerializeField] GameObject pauseMenu;
    [SerializeField] InGameUI inGameUI;

    public bool gameStarted = false; // keeping track of the state of the menu
    bool menuOpen = false; // ingredients shown at game start

    void Start()
    {
        Time.timeScale = 0f;
        canvas.SetActive(true);
    }

    void Update()
    {
        ShowIngredients();
        ShowServeDrinkUI();
        OpenPauseMenu();
    }

    public void OnStartClicked()
    {
        gameStarted = true;
        Time.timeScale = 1f;
        startIngredientUI.SetActive(false);
    }

    public void OnNoClicked()
    {
        Time.timeScale = 1f;
        serveDrinkPromptUI.SetActive(false);
    }

    void ShowIngredients()
    {
        bool ingredientListActive = false;

        if (Input.GetKey(KeyCode.Tab))
        {
            ingredientListActive = !ingredientListActive;
        }

        if (ingredientListActive)
        {
            inGameIngredientList.SetActive(true);
        }
        else
        {
            inGameIngredientList.SetActive(false);
        }
    }

    void ShowServeDrinkUI()
    {
        if (gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                menuOpen = !menuOpen;
            }

            if (menuOpen)
            {
                serveDrinkPromptUI.SetActive(true);
            }
            else
            {
                Time.timeScale = 1f;
                serveDrinkPromptUI.SetActive(false);
            }
        }
    }

    void OpenPauseMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && gameStarted)
        {
            pauseMenuOpen = !pauseMenuOpen;
        }

        if (pauseMenuOpen)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else if (!pauseMenuOpen && gameStarted)
        {
            Time.timeScale = 1f;
            pauseMenu.SetActive(false);
        }
    }
}
