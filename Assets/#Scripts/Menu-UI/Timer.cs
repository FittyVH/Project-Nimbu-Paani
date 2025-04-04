using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour
{
    public float timeLimit = 60f;
    public TMP_Text timerText;
    [SerializeField] GameObject timesUpPrompt;

    private float remainingTime;

    void Start()
    {
        remainingTime = timeLimit;
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            remainingTime = 0;
            TimerEnded();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    void TimerEnded()
    {
        timesUpPrompt.SetActive(true);
        Invoke(nameof(LoadGameOverScene), 1f);
        Debug.Log("Time's up!");
    }

    void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}
