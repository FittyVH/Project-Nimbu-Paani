using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class LevelSelector : MonoBehaviour
{
    // ui elements
    [Header ("UI elements")]
    [SerializeField] RectTransform popUI;
    [SerializeField] float popInTime = 0.7f;
    [SerializeField] float hoverZoomTime = 0.2f;
    [SerializeField] String sceneName;

    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    [SerializeField] AudioClip pageTurn;
    [SerializeField] AudioSource secAudioSrc;

    RectTransform rectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    // hover animations
    public void OnHoverEnter()
    {
        rectTransform.DOScale(new Vector2(1.2f, 1.2f), hoverZoomTime);
    }

    public void OnHoverExit()
    {
        rectTransform.DOScale(new Vector2(1f, 1f), hoverZoomTime);
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
        StartCoroutine(TransitionLoader());
        secAudioSrc.clip = pageTurn;
        secAudioSrc.Play();
    }
    
    IEnumerator TransitionLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(transitionTime);
        SceneManager.LoadScene(sceneName);
    }
}

