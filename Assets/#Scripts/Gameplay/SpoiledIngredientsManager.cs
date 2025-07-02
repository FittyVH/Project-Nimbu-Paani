using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class SpoiledIngredientsManager : MonoBehaviour
{
    //spoiled ingredient manager
    [Header("spoiled ingredient manager")]
    [SerializeField] List<GameObject> correctSpoiledIngredients; // how many player can afford to loose
    public List<string> currentSpoiledIngredients;
    List<string> compareIngredients = new List<string>();
    bool listIsEqual;

    //ui
    [Header("Ui")]
    [SerializeField] RectTransform restartPopUp;
    [SerializeField] float popIntime = 0.7f;
    bool animationStarted = false;

    void Start()
    {
        foreach (var ingredient in correctSpoiledIngredients)
        {
            compareIngredients.Add(ingredient.tag);
        }
    }

    void Update()
    {
        listIsEqual = compareIngredients.OrderBy(x => x).SequenceEqual(currentSpoiledIngredients.OrderBy(x => x)); // check if equal

        if (listIsEqual && !animationStarted)
        {
            animationStarted = true;
            RestartAnimationIn();
            Invoke(nameof(RestartAnimationOut), 4f);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (compareIngredients.Contains(other.gameObject.tag))
        {
            Debug.Log("detected");
            currentSpoiledIngredients.Add(other.gameObject.tag);
        }
    }

    void RestartAnimationIn()
    {
        restartPopUp.DOAnchorPos(new Vector2(0f, 0f), popIntime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
    }
    void RestartAnimationOut()
    {
        restartPopUp.DOAnchorPos(new Vector2(0f, -1000f), popIntime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
    }
}
