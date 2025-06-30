using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public Animator transitionAnimator;
    [SerializeField] float transitionTime = 0.5f;

    [SerializeField] AudioClip pageTurn;
    [SerializeField] AudioSource secAudioSrc;

    [Header("sub-menus")]
    [SerializeField] float popInTime = 0.7f;
    [SerializeField] RectTransform settingsPanel;

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
    public void OnSettingsClicked()
    {
        settingsPanel.DOAnchorPos(new Vector2(0f, 0f), popInTime, false).SetEase(Ease.InOutQuint);
    }
    public void OnBackSettingsClicked()
    {
        settingsPanel.DOAnchorPos(new Vector2(0f, -2000f), popInTime, false).SetEase(Ease.InOutQuint);
    }

    IEnumerator TransitionLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}