using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    int levelsCompleted;

    public GameObject[] levelButtons;

    void Start()
    {
        levelsCompleted = PlayerPrefs.GetInt("levelsCompleted", 1);
        Debug.Log(PlayerPrefs.GetInt("levelsCompleted"));

        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].SetActive(false);
        }

        for (int i = 0; i < levelsCompleted; i++)
        {
            levelButtons[i].SetActive(true);
        }
    }
}
