using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceSimulatorConstant : MonoBehaviour
{
    public static DeviceSimulatorConstant Instance { get; private set; }
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
