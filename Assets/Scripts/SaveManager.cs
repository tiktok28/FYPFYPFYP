using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour
{
    public static Save Instance {get; private set;}
    private SaveFile currentSave = null;
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
    
    public void LoadSave(SaveFile saveFile)
    {
        currentSave = saveFile;
    }
    public bool CheckSave()
    {
        if(currentSave != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
