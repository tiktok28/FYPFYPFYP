using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject menu;
    public InputActionProperty showButton;

    // Update is called once per frame
    void Update()
    {
        if(showButton.action.WasPressedThisFrame() && SceneManager.GetActiveScene().name=="Classroom")
        {
            menu.SetActive(!menu.activeSelf);
            if(menu.activeSelf)
            {
                GameManager.Instance.PauseGame();
                Debug.Log("Paused");
            }
            else
            {
                GameManager.Instance.ResumeGame();
                Debug.Log("Resumed");
            }
        }
    }
}
