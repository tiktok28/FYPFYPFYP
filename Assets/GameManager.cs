using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject textManager;

    public int lectureMode; //0 = Light, 1 = Electricity

    public int demonstrationMode; //0 = Light, 1 = Electricity

    public int assessmentMode; //0 = Light, 1 = Electricity

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
        if(demonstrationMode == 0)
        {
            
        }   
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
        
    }
}
