using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    [SerializeField] GameObject popUI;
    [SerializeField] String sceneName;

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
        SceneManager.LoadScene(sceneName);
    }
}

