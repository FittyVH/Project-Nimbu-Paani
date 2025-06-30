using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class IngredientsManager : MonoBehaviour
{
    // ingredient manager
    [SerializeField] List<GameObject> correctIngredients;
    public List<string> currentIngredients;
    List<string> compareIngredients = new List<string>();

    [SerializeField] int currentLevel;

    // game over UI components
    [Header("game over UI components")]
    [SerializeField] CanvasGroup gameOverUI;
    [SerializeField] RectTransform blackScreenGameOver;
    [SerializeField] float popIntime = 0.7f;
    [SerializeField] float gameOverTime = 2f;

    // victory UI components
    [Header("victory UI components")]
    [SerializeField] RectTransform blackScreenVictory;
    [SerializeField] RectTransform servingDrinkYou;
    [SerializeField] RectTransform servingDrinkCustomer;
    [SerializeField] RectTransform successCustomer;
    [SerializeField] GameObject victoryText;

    // others
    [Header("next scene name")]
    [SerializeField] string nextSceneName;

    // audio
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pageTurnAudio;

    // bools
    bool listIsEqual;

    void Start()
    {
        foreach (var ingredient in correctIngredients)
        {
            compareIngredients.Add(ingredient.tag);
        }
    }

    void Update()
    {
        listIsEqual = compareIngredients.OrderBy(x => x).SequenceEqual(currentIngredients.OrderBy(x => x));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cat Treat Box")
        {
            return;
        }
        else
        {
            currentIngredients.Add(other.gameObject.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        currentIngredients.Remove(other.gameObject.tag);
    }

    // serve drink clicked
    public void OnYesClicked()
    {
        if (listIsEqual)
        {
            if (nextSceneName != null)
            {
                SceneManager.LoadScene(nextSceneName);
            }

            PageTurnAudio();
            StartCoroutine(VictoryRoutine());

            if (currentLevel >= PlayerPrefs.GetInt("levelsCompleted"))
            {
                PlayerPrefs.SetInt("levelsCompleted", currentLevel + 1);
            }
            Debug.Log(PlayerPrefs.GetInt("levelsCompleted"));
        }
        else
        {
            PageTurnAudio();
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
        BlackScreenAnimationsGameOver();
        yield return new WaitForSeconds(popIntime);
        GameOverUIFadeIn();
        yield return new WaitForSeconds(gameOverTime);
        GameOverUIFadeOut();
        yield return new WaitForSeconds(popIntime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator VictoryRoutine()
    {
        BlackScreenAnimationVictory();
        yield return new WaitForSeconds(popIntime);
        ServingDrinkYouAnimation();
        yield return new WaitForSeconds(popIntime);
        ServingDrinkCustomerAnimation();
        yield return new WaitForSeconds(popIntime + 0.7f);
        ServingDrinkCustomerOutAnimation();
        ServingDrinkYouOutAnimation();
        yield return new WaitForSeconds(popIntime);
        SuccessCustomerAnimation();
        yield return new WaitForSeconds(popIntime + 0.7f);
        VictoryTextAnimation();
    }

    // game over UI animations
    void BlackScreenAnimationsGameOver()
    {
        blackScreenGameOver.transform.localPosition = new Vector2(2100f, 0f);
        blackScreenGameOver.DOAnchorPos(new Vector2(0f, 0f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void GameOverUIFadeIn()
    {
        gameOverUI.DOFade(1, popIntime);
    }

    void GameOverUIFadeOut()
    {
        gameOverUI.DOFade(0, popIntime);
    }

    // victory UI animations
    void BlackScreenAnimationVictory()
    {
        blackScreenVictory.transform.localPosition = new Vector2(2100f, 0f);
        blackScreenVictory.DOAnchorPos(new Vector2(0f, 0f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void ServingDrinkYouAnimation()
    {
        servingDrinkYou.transform.localPosition = new Vector2(-500f, 1000f);
        servingDrinkYou.DOAnchorPos(new Vector2(-500f, -100f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void ServingDrinkCustomerAnimation()
    {
        servingDrinkCustomer.transform.localPosition = new Vector2(500f, -1000f);
        servingDrinkCustomer.DOAnchorPos(new Vector2(500f, 100f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void ServingDrinkYouOutAnimation()
    {
        servingDrinkYou.DOAnchorPos(new Vector2(-500f, 1000f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void ServingDrinkCustomerOutAnimation()
    {
        servingDrinkCustomer.DOAnchorPos(new Vector2(500f, -1000f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void SuccessCustomerAnimation()
    {
        successCustomer.transform.localPosition = new Vector2(2000f, 0f);
        successCustomer.DOAnchorPos(new Vector2(500f, 0f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void VictoryTextAnimation()
    {
        victoryText.SetActive(true);
        victoryText.GetComponent<CanvasGroup>().DOFade(1, popIntime);
    }

    void PageTurnAudio()
    {
        audioSource.clip = pageTurnAudio;
        audioSource.Play();
    }
}
