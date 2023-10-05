using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera initialCamera; // Reference to the starting camera
    public Camera secondaryCamera; // Reference to the secondary camera

    void Start()
    {
        // Disable the secondary camera at the beginning
        secondaryCamera.gameObject.SetActive(false);
    }

    public void SwitchCameras()
    {
        // Enable the secondary camera and disable the initial camera
        secondaryCamera.gameObject.SetActive(true);
        initialCamera.gameObject.SetActive(false);
    }
}
