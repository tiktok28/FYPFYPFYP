using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Rendering.Universal.Internal;

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
        if(!SceneManager.GetSceneByName("Classroom").isLoaded){
            SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        }
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
    public IEnumerator LightConcepts()
    {
        GameManager.Instance.lectureMode = 0;
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "Lecture"){
                sceneObjs[i].SetActive(true);
            }
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
        SceneManager.SetActiveScene(classroom);
    }
    public IEnumerator ElectricityConcepts()
    {
        GameManager.Instance.lectureMode = 1;
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "Lecture"){
                sceneObjs[i].SetActive(true);
            }
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
        SceneManager.SetActiveScene(classroom);
    }
    public void DemonstrationsButton()
    {
        if(!SceneManager.GetSceneByName("Classroom").isLoaded){
            SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        }
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
    public IEnumerator LightDemonstrations()
    {
        GameManager.Instance.demonstrationMode = 0;
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "Demonstration"){
                sceneObjs[i].SetActive(true);
            }
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
        SceneManager.SetActiveScene(classroom);
    }
    public IEnumerator ElectricityDemonstrations()
    {
        GameManager.Instance.demonstrationMode = 1;
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "Demonstration"){
                sceneObjs[i].SetActive(true);
            }
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
        SceneManager.SetActiveScene(classroom);
    }
    public void AssessmentsButton()
    {
        if(!SceneManager.GetSceneByName("Classroom").isLoaded){
            SceneManager.LoadScene("Classroom", LoadSceneMode.Additive);
        }
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
    public IEnumerator Assessments()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject mmXR = GameObject.Find("XR Origin (Main Menu)");
        mmXR.SetActive(false);
        Scene classroom = SceneManager.GetSceneByName("Classroom");
        var sceneObjs = classroom.GetRootGameObjects();
        for (int i = 0; i < sceneObjs.Length; i++){
            if(sceneObjs[i].name == "Assessment"){
                sceneObjs[i].SetActive(true);
            }
            if(sceneObjs[i].name == "XR Origin (Classroom)"){
                sceneObjs[i].SetActive(true);
            }
        }
        SceneManager.SetActiveScene(classroom);
    }
    public void LightConceptsButton()
    {
        StartCoroutine(LightConcepts());
    }
    public void ElectricityConceptsButton()
    {
        StartCoroutine(ElectricityConcepts());
    }
    public void LightDemonstrationsButton()
    {
        StartCoroutine(LightDemonstrations());
    }
    public void ElectricityDemonstrationsButton()
    {
        StartCoroutine(ElectricityDemonstrations());
    }
        public void LightAssessmentsButton()
    {
        StartCoroutine(Assessments());
    }
    public void ElectricityAssessmentsButton()
    {
        StartCoroutine(Assessments());
    }
    public void ResumeButton()
    {
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        GameObject menuCanvas = pauseMenu.transform.Find("MenuCanvas").gameObject;
        menuCanvas.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
    public void SaveNExitButton()
    {
        GameObject pauseMenu = GameObject.Find("PauseMenu");
        GameObject menuCanvas = pauseMenu.transform.Find("MenuCanvas").gameObject;
        menuCanvas.SetActive(false);
        GameManager.Instance.ResumeGame();
    }
    public IEnumerator LeavingDemonstrations()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("XR Origin (Classroom)").SetActive(false);
        GameObject.Find("Demonstration").SetActive(false);
        GameObject.Find("XR Origin (Main Menu)").SetActive(true);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Menu"));
    }
    public IEnumerator LeavingLectures()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject.Find("XR Origin (Classroom)").SetActive(false);
        GameObject.Find("Demonstration").SetActive(false);
        GameObject.Find("XR Origin (Main Menu)").SetActive(true);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("Main Menu"));
    }
}
