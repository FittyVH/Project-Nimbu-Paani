using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject popUI;
    [SerializeField] String sceneName;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    [SerializeField] AudioClip pageTurn;
    [SerializeField] AudioSource secAudioSrc;

    void Start()
    {
        popUI.SetActive(false);
    }

    public void LevelClick()
    {
        popUI.SetActive(true);
    }
    public void OnBackClicked()
    {
        popUI.SetActive(false);
    }
    public void OnPlayClicked()
    {
        StartCoroutine(TransitinLoader());
        secAudioSrc.clip = pageTurn;
        secAudioSrc.Play();
    }
    
    IEnumerator TransitinLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}

