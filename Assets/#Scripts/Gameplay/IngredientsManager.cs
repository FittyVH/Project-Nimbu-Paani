using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IngredientsManager : MonoBehaviour
{
    [SerializeField] List<GameObject> correctIngredients;
    public List<string> currentIngredients;
    List<string> compareIngredients = new List<string>();

    [SerializeField] string nextSceneName;

    [SerializeField] GameObject gameOverUI;

    bool listIsEqual;

    void Start()
    {
        gameOverUI.SetActive(false);

        foreach(var ingredient in correctIngredients)
        {
            compareIngredients.Add(ingredient.tag);
        }
    }

    void Update()
    {
        listIsEqual = compareIngredients.OrderBy(x => x).SequenceEqual(currentIngredients.OrderBy(x => x));

        if (gameOverUI.activeSelf)
        {
            Debug.Log("game Over");
            StartCoroutine(RestartScene());
            // Invoke(nameof(RestartScene), 1.5f);
            // RestartScene();
        }
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
            Time.timeScale = 1f;
            // Debug.Log("sahi h");
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Time.timeScale = 1f;
            gameOverUI.SetActive(true);
            // StartCoroutine(RestartScene());
        }
    }

    // void RestartScene()
    // {
    //     gameOverUI.SetActive(false);
    //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    // }

    public IEnumerator RestartScene()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("started coroutine");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
