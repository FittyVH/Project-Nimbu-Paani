using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] RectTransform popUI;
    [SerializeField] float popInTime = 0.7f;
    [SerializeField] String sceneName;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    [SerializeField] AudioClip pageTurn;
    [SerializeField] AudioSource secAudioSrc;

    void Start()
    {
        // popUI.SetActive(false);
    }

    public void LevelClick()
    {
        // popUI.SetActive(true);
        popUI.transform.localPosition = new Vector2(0f, -2000f);
        popUI.DOAnchorPos(new Vector2(0f, 0f), popInTime, true).SetEase(Ease.InOutQuint);
    }
    public void OnBackClicked()
    {
        // popUI.SetActive(false);
        popUI.transform.localPosition = new Vector2(0f, 0f);
        popUI.DOAnchorPos(new Vector2(0f, -2000f), popInTime, false).SetEase(Ease.InOutQuint);
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

