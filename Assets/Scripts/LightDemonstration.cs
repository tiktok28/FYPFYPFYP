using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class LightDemonstration : MonoBehaviour
{    
    public GameObject laser;
    public GameObject prism;
    public GameObject measuringTool;
    public GameObject boardText;
    private int count;
    private Boolean readyForEvent = false;
    void Awake()
    {
        PageSkipper.FullTextCompleted += OffLaser;
        count = boardText.GetComponent<TextManager>().demonstrationCount;
    }

    // Update is called once per frame
    void Update()
    {
        if(count != boardText.GetComponent<TextManager>().demonstrationCount)
        {
            count++;
            readyForEvent = true;
        }
        if(count == 2 && readyForEvent)
        {
            laser.SetActive(true);
            readyForEvent = false;
        }
        if(count == 3 && readyForEvent)
        {
            prism.SetActive(true);
            readyForEvent = false;
        }
        if(count == 4 && readyForEvent)
        {
            measuringTool.SetActive(true);
            readyForEvent = false;
        }
        if(count == 8 && readyForEvent)
        {
            laser.GetComponent<XRGrabInteractable>().enabled = false;
            laser.GetComponent<Rigidbody>().isKinematic = true;
            laser.GetComponent<ShootLaser>().forceOn();
            prism.GetComponent<XRGrabInteractable>().enabled = false;
            prism.GetComponent<Rigidbody>().isKinematic = true;
            measuringTool.GetComponent<XRGrabInteractable>().enabled = false;
            measuringTool.GetComponent<Rigidbody>().isKinematic = true;
            StartCoroutine(Event8());
            readyForEvent = false;
        }
        if(count == 9 && readyForEvent)
        {
            StartCoroutine(Event9());
            readyForEvent = false;
        }
    }
    void OffLaser()
    {
        laser.GetComponent<ShootLaser>().forceOff();
    }
    private IEnumerator Event8() 
    {
        float totalMovementTime = 5f; //the amount of time you want the movement to take
        float currentMovementTime = 0f;//The amount of time that has passed
        Vector3 laserDestination = new Vector3(-0.399995804f,-0.652500153f,0.700000048f);
        Quaternion laserRotation = Quaternion.Euler(0,0,0);
        Vector3 prismDestination = new Vector3(0.0429992676f,-0.614999771f,0.713999987f);
        Quaternion prismRotation = Quaternion.Euler(0, 46.05f, 0);
        Vector3 measuringToolDestination = new Vector3(-0.0151023865f,-0.663974762f,0.700896025f);
        Quaternion measuringToolRotation = Quaternion.Euler(0.405465066f,45.813858f,-0.00483300025f);

        while (Vector3.Distance(prism.transform.localPosition, prismDestination) > 0) 
        {
            currentMovementTime += Time.deltaTime;
            prism.transform.localPosition = Vector3.Lerp(prism.transform.localPosition, prismDestination, currentMovementTime / totalMovementTime);
            prism.transform.rotation = Quaternion.Lerp(prism.transform.rotation, prismRotation, currentMovementTime / totalMovementTime);
            yield return null;
        }
        currentMovementTime = 0f;
        while (Vector3.Distance(laser.transform.localPosition, laserDestination) > 0) 
        {
            currentMovementTime += Time.deltaTime;
            laser.transform.localPosition = Vector3.Lerp(laser.transform.localPosition, laserDestination, currentMovementTime / totalMovementTime);
            laser.transform.rotation = Quaternion.Lerp(laser.transform.rotation, laserRotation, currentMovementTime / totalMovementTime);
            yield return null;
        }
        currentMovementTime = 0f;
        while (Vector3.Distance(measuringTool.transform.localPosition, measuringToolDestination) > 0) 
        {
            currentMovementTime += Time.deltaTime;
            measuringTool.transform.localPosition = Vector3.Lerp(measuringTool.transform.localPosition, measuringToolDestination, currentMovementTime / totalMovementTime);
            measuringTool.transform.rotation = Quaternion.Lerp(measuringTool.transform.rotation, measuringToolRotation, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }
    private IEnumerator Event9()
    {
        Vector3 measuringToolDestination = new Vector3(-0.0126f,-0.564800024f,0.702300012f);
        Quaternion measuringToolRotation = Quaternion.Euler(0.391001701f,225f,359.98999f);
        float totalMovementTime = 1f;
        float currentMovementTime = 0f;
        while (Vector3.Distance(measuringTool.transform.localPosition, measuringToolDestination) > 0) 
        {
            currentMovementTime += Time.deltaTime;
            measuringTool.transform.localPosition = Vector3.Lerp(measuringTool.transform.localPosition, measuringToolDestination, currentMovementTime / totalMovementTime);
            measuringTool.transform.rotation = Quaternion.Lerp(measuringTool.transform.rotation, measuringToolRotation, currentMovementTime / totalMovementTime);
            yield return null;
        }
    }
}
