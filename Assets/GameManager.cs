using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lectureMode; //0 = Light, 1 = Electricity

    public int demonstrationMode; //0 = Light, 1 = Electricity

    public int assessmentMode; //0 = Light, 1 = Electricity

    private void Awake()
    {
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
        var sceneObjs = SceneManager.GetActiveScene().GetRootGameObjects();
        if(Input.GetKeyDown(KeyCode.Escape) && (SceneManager.GetActiveScene().name=="Classroom"))
        {
            GameObject menuCanvas = null;
            for (int i = 0; i < sceneObjs.Length; i++)
            {
                if(sceneObjs[i].name == "MenuCanvas")
                {
                    menuCanvas = sceneObjs[i];
                }
            }
            if(menuCanvas != null)
            {
                if(!menuCanvas.activeInHierarchy)
                {
                    menuCanvas.SetActive(true);
                    PauseGame();
                }
                else
                {
                    menuCanvas.SetActive(false);
                    ResumeGame();
                }
            }

        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1;
    }
}
