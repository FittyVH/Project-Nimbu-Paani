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

    // game over UI components
    [Header("game over UI components")]
    [SerializeField] CanvasGroup gameOverUI;
    [SerializeField] RectTransform blackScreen;
    [SerializeField] float popIntime = 0.7f;
    [SerializeField] float gameOverTime = 2f;

    // others
    [Header("next scene name")]
    [SerializeField] string nextSceneName;

    // audio
    [Header ("Audio")]
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
        currentIngredients.Add(other.gameObject.tag);
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
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            PageTurnAudio();
            StartCoroutine(GameOverRoutine());
        }
    }

    IEnumerator GameOverRoutine()
    {
        BlackScreenAnimations();
        yield return new WaitForSeconds(popIntime);
        GameOverUIFadeIn();
        yield return new WaitForSeconds(gameOverTime);
        GameOverUIFadeOut();
        yield return new WaitForSeconds(popIntime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void BlackScreenAnimations()
    {
        blackScreen.transform.localPosition = new Vector2(2100f, 0f);
        blackScreen.DOAnchorPos(new Vector2(0f, 0f), popIntime, false).SetEase(Ease.InOutQuint);
    }

    void GameOverUIFadeIn()
    {
        gameOverUI.DOFade(1, popIntime);
    }

    void GameOverUIFadeOut()
    {
        gameOverUI.DOFade(0, popIntime);
    }

    void PageTurnAudio()
    {
        audioSource.clip = pageTurnAudio;
        audioSource.Play();
    }
}
