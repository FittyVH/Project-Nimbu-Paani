using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    //other menus
    [Header ("Other menus")]
    [SerializeField] GameObject inGameIngredientList;
    [SerializeField] GameObject serveDrinkPromptUI;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject pauseMenu;

    //start ingredient UI components
    [Header("start ingredient UI components")]
    [SerializeField] RectTransform startIngredientUI;
    [SerializeField] RectTransform blackRimUp;
    [SerializeField] RectTransform blackRimDown;
    [SerializeField] CanvasGroup alpha;
    [SerializeField] float popInTime = 0.7f;

    // script references
    [Header ("script references")]
    [SerializeField] InGameUI inGameUI;

    // bools
    public bool gameStarted = false; // keeping track of the state of the menu
    public bool pauseMenuOpen = false;
    bool serveDrinkMenuOpen = false;

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

        // ui animations
        blackRimUp.transform.localPosition = new Vector2(0f, 597f);
        blackRimUp.DOAnchorPos(new Vector2(0f, 900f), popInTime, false).SetEase(Ease.InOutQuint);

        blackRimDown.transform.localPosition = new Vector2(0f, -617f);
        blackRimDown.DOAnchorPos(new Vector2(0f, -900f), popInTime, false).SetEase(Ease.InOutQuint);

        startIngredientUI.transform.localPosition = new Vector2(0f, 0f);
        startIngredientUI.DOAnchorPos(new Vector2(1800f, 0f), popInTime, false).SetEase(Ease.InOutQuint);

        alpha.DOFade(0, popInTime);
    }

    public void OnNoClicked()
    {
        Time.timeScale = 1f;
        serveDrinkPromptUI.SetActive(false);
    }

    public void OnNextClicked()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    void ShowIngredients()
    {
        bool ingredientListActive = false;

        if (gameStarted)
        {
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
    }

    void ShowServeDrinkUI()
    {
        if (gameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                serveDrinkMenuOpen = !serveDrinkMenuOpen;
            }

            if (serveDrinkMenuOpen)
            {
                serveDrinkPromptUI.SetActive(true);
            }
            else
            {
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
