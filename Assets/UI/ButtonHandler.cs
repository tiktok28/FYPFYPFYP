using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClick()
    {
        // Your code to execute when the button is clicked
        Debug.Log("Button Clicked!");
    }

    public void StartButton()
    {
        Scene classroom;
        GameObject menuXR = GameObject.Find("XR Origin (Main Menu)");
        menuXR.SetActive(false);
        classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
    }
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
}
