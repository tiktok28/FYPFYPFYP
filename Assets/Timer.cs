using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 600;
    public float initialTime = 600;
    public bool timerIsRunning = false;
    public float timeElapsed = 0;
    public TextMeshPro timeText;

    void Start()
    {
        // DisplayTime(timeRemaining);
    }
    void Update()
    {
        if (timerIsRunning)
        {
            DisplayTime(timeRemaining);
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                timeElapsed += Time.deltaTime;
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                ButtonHandler.LeavingAssessments();
            }
        }
    }
    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);  
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StartTimer()
    {
        timerIsRunning = true;
    }
    public void StopTimer()
    {
        timerIsRunning = false;
    }
    float TimeTaken()
    {
        return initialTime - timeRemaining;
    }
}