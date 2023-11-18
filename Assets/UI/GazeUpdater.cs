using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeUpdater : MonoBehaviour
{
    [SerializeField] private Camera camera;
    private RectTransform pos;
    public float CameraDistance = 3.0F;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(camera.transform.position);
        // Debug.Log(camera.transform.rotation);
        // Define my target position in front of the camera ->
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, CameraDistance));
    
        // Smoothly move my object towards that position ->
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime, Mathf.Infinity, Time.unscaledDeltaTime);
    
        // version 1: my object's rotation is always facing to camera with no dampening  ->
        // transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        transform.LookAt(camera.transform);
    
        // version 2 : my object's rotation isn't finished synchronously with the position smooth.damp ->
        // transform.rotation = Quaternion.RotateTowards(transform.rotation, target.rotation, 35 * Time.deltaTime);

    }

    void OnEnable()
    {
        Debug.Log("Enabled");
        Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, CameraDistance));
    
        // Smoothly move my object towards that position ->
        transform.position = targetPosition;
    
        // version 1: my object's rotation is always facing to camera with no dampening  ->
        // transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
        transform.LookAt(camera.transform);
    }

    void OnDisable()
    {
        Debug.Log("Disabled");
    }

    void Awake()
    {
        target = camera.transform;
        // pos = GetComponent<RectTransform>();
        // GameObject xr = GameObject.Find("XR Origin (Classroom)");
        // camera = xr.transform.Find("Camera Offset/Main Camera");
    }
}
