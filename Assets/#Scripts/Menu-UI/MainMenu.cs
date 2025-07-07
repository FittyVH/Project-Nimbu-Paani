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
    [SerializeField] RectTransform creditsPanel;
    [SerializeField] GameObject creditsText;

    bool isFullScreen = true;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }

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
    public void SwitchScreenMode()
    {
        if (isFullScreen)
        {
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }
        else if (!isFullScreen)
        {
            Screen.fullScreen = true;
            Screen.SetResolution(1920, 1080, true);
        }
        isFullScreen = !isFullScreen;
    }
    public void OnCreditsClicked()
    {
        StartCoroutine(CreditsRoll());
    }
    public void OnBackCreditsClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator TransitionLoader()
    {
        transitionAnimator.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator CreditsRoll()
    {
        creditsText.GetComponent<CanvasGroup>().alpha = 0f;
        creditsPanel.DOAnchorPos(new Vector2(0f, 0f), popInTime, false).SetEase(Ease.InOutQuint);
        yield return new WaitForSeconds(1.5f);
        creditsText.GetComponent<CanvasGroup>().DOFade(1f, 3f);
        yield return new WaitForSeconds(2f);
        creditsText.GetComponent<RectTransform>().DOAnchorPos(new Vector2(0f, 3000f), 30f, false);
    }
}