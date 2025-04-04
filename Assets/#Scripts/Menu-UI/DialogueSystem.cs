using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pageTurnAudio;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    public GameObject[] comic;
    int i = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0)) && i < comic.Length - 1)
        {
            PlayAudio();
            comic[i].SetActive(false);
            i++;
            comic[i].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(TransitinLoader());
            audioSource.clip = pageTurnAudio;
            audioSource.Play();
        }
    }

    void PlayAudio()
    {
        audioSource.clip = pageTurnAudio;
        audioSource.Play();
    }

    IEnumerator TransitinLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
