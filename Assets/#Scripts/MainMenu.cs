using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    [SerializeField] AudioClip pageTurn;
    [SerializeField] AudioSource secAudioSrc;

    public void OnPlayClicked()
    {
        StartCoroutine(TransitionLoader());
        secAudioSrc.clip = pageTurn;
        secAudioSrc.Play();
    }
    public void OnQuitClicked()
    {
        Application.Quit();
    }

    IEnumerator TransitionLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}