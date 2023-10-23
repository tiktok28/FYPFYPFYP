using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonHandler : MonoBehaviour
{
    public void OnButtonClick()
    {
        // Your code to execute when the button is clicked
        Debug.Log("Button Clicked!");
    }
    public void StartButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        var currentObjs = currentScene.GetRootGameObjects();

        for (int i = 0; i < currentObjs.Length; i++){
            if(currentObjs[i].name == "MMCanvas"){
                currentObjs[i].SetActive(false);
            }
            if(currentObjs[i].name == "StartCanvas"){
                currentObjs[i].SetActive(true);
            }
        }
    }
    public void LoadSaveButton()
    {
        Debug.Log("Load save button");
    }
    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Quit!");
    }
    public void ConceptsButton()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        var currentObjs = currentScene.GetRootGameObjects();
        for (int i = 0; i < currentObjs.Length; i++){
            if(currentObjs[i].name == "StartCanvas"){
                currentObjs[i].SetActive(false);
            }
            if(currentObjs[i].name == "ConceptsCanvas"){
                currentObjs[i].SetActive(true);
            }
        }
        
    }
    IEnumerator Concepts()
    {
        SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
    }
    public void DemonstrationsButton()
    {
        SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        Scene currentScene = SceneManager.GetActiveScene();
        var currentObjs = currentScene.GetRootGameObjects();

        for (int i = 0; i < currentObjs.Length; i++){
            if(currentObjs[i].name == "StartCanvas"){
                currentObjs[i].SetActive(false);
            }
            if(currentObjs[i].name == "DemonstrationsCanvas"){
                currentObjs[i].SetActive(true);
            }
        }
        
    }
    IEnumerator Demonstrations()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
    }
    public void AssessmentsButton()
    {
        SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        Scene currentScene = SceneManager.GetActiveScene();
        var currentObjs = currentScene.GetRootGameObjects();

        for (int i = 0; i < currentObjs.Length; i++){
            if(currentObjs[i].name == "StartCanvas"){
                currentObjs[i].SetActive(false);
            }
            if(currentObjs[i].name == "AssessmentsCanvas"){
                currentObjs[i].SetActive(true);
            }
        }
        
    }
    IEnumerator Assessments()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
    }
    public void LightConceptsButton()
    {
        StartCoroutine(Concepts());
    }
    public void ElectricityConceptsButton()
    {
        StartCoroutine(Concepts());
    }
        public void LightDemonstrationsButton()
    {
        StartCoroutine(Demonstrations());
    }
    public void ElectricityDemonstrationsButton()
    {
        StartCoroutine(Demonstrations());
    }
        public void LightAssessmentsButton()
    {
        StartCoroutine(Assessments());
    }
    public void ElectricityAssessmentsButton()
    {
        StartCoroutine(Assessments());
    }
}
