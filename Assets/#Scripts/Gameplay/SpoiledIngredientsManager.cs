using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using DG.Tweening;

public class SpoiledIngredientsManager : MonoBehaviour
{
    [System.Serializable]
    public class Cube2Limit
    {
        public int limit;       // The tag of the fruit
        public GameObject cube;  // The cube to spawn for this fruit
    }

    public List<Cube2Limit> cube2LimitList;

    private Dictionary<string, int> cube2LimitDict;

    //ui
    [Header("Ui and audio")]
    [SerializeField] RectTransform restartPopUp;
    [SerializeField] float popIntime = 0.7f;
    bool animationStarted = false;

    [SerializeField] AudioClip woosh;
    [SerializeField] AudioSource audioSrc;

    void Start()
    {
        cube2LimitDict = new Dictionary<string, int>(); // dict initiaized
        foreach (var map in cube2LimitList)
        {
            // fill the dictionary using List of the serialized class
            cube2LimitDict.Add(map.cube.tag, map.limit);
        }
    }

    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (cube2LimitDict.ContainsKey(other.gameObject.tag))
        {
            cube2LimitDict[other.gameObject.tag]--; // decrement limit value
            Debug.Log(cube2LimitDict[other.gameObject.tag]);

            if (cube2LimitDict.ContainsValue(0))
            {
                Debug.Log("restart");
                StartCoroutine(RestartMenuPop());
            }
        }
    }

    IEnumerator RestartMenuPop()
    {
        audioSrc.clip = woosh;
        audioSrc.Play();
        restartPopUp.DOAnchorPos(new Vector2(0f, 0f), popIntime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
        yield return new WaitForSeconds(3.5f);
        audioSrc.clip = woosh;
        audioSrc.Play();
        restartPopUp.DOAnchorPos(new Vector2(0f, -1000f), popIntime, false).SetEase(Ease.InOutQuint).SetUpdate(true);
    }
}
