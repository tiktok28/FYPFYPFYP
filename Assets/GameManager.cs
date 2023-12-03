using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int lectureMode; //0 = Light, 1 = Electricity

    public int demonstrationMode; //0 = Light, 1 = Electricity

    public int assessmentMode; //0 = Light, 1 = Electricity
    public float timeElapsed;

    private void Awake()
    {
        PageSkipper.FullTextCompleted += returnToMenu;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;
    }

    void Start()
    {
        timeElapsed = 0;
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }
    private void returnToMenu()
    {
        StartCoroutine(returnToMenuWithDelay());
    }
    private IEnumerator returnToMenuWithDelay()
    {
        yield return new WaitForSeconds(1f);
        if(GameObject.Find("Lecture").activeInHierarchy)
        {
            StartCoroutine(ButtonHandler.LeavingLectures());
        }
        if(GameObject.Find("Demonstration").activeInHierarchy)
        {
            StartCoroutine(ButtonHandler.LeavingDemonstrations());
        }
    }
}
