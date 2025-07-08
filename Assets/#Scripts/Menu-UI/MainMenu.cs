using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Linq;
using TMPro;

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
    [SerializeField] TMP_Dropdown resolutionDropDown;

    Resolution[] resolutions;

    bool isFullScreen = true;

    void Awake()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropDown.ClearOptions();
        List<String> resOptions = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "X" + resolutions[i].height;
            resOptions.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropDown.AddOptions(resOptions);
        resolutionDropDown.value = currentResolutionIndex;
        resolutionDropDown.RefreshShownValue();
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
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, FullScreenMode.Windowed);
        }
        else if (!isFullScreen)
        {
            Screen.fullScreen = true;
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        isFullScreen = !isFullScreen;
    }
    public void SetResolution(int resIndex)
    {
        Resolution resolution = resolutions[resIndex];
        Screen.SetResolution(resolution.width, resolution.height, isFullScreen);
    }

    public void OnCreditsClicked()
    {
        StartCoroutine(CreditsRoll());
    }
    public void OnBackCreditsClicked()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Res1920()
    {
        Screen.SetResolution(1920, 1080, isFullScreen);
    }
    public void Res1600()
    {
        Screen.SetResolution(1600, 900, isFullScreen);
    }
    public void Res1360()
    {
        Screen.SetResolution(1360, 768, isFullScreen);
    }
    public void Res1280()
    {
        Screen.SetResolution(1280, 720, isFullScreen);
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