using System;
using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public Material material;
    [SerializeField] public float laserSize = 0.03f;
    [SerializeField] public float distanceToCollider = 1.5f;
    [SerializeField] public float distanceFromCollider = 1.6f;
    [SerializeField] public float tinyDistance1 = 0.0001f;
    [SerializeField] public float tinyDistance2 = 0.001f;
    LaserBeam beam;
    private Boolean state = false; //False = Off, True = On

    // Update is called once per frame
    // private void Start()
    // {
    //     beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserSize);
    // }
    // void Update()
    // {
    //     beam.laser.positionCount = 0;
    //     beam.laserIndices.Clear();
    //     beam.CastRay(transform.position, transform.right, beam.laser);
    // }

    void OnEnable()
    {
        state = false;
    }

    void OnDisable()
    {
        state = false;
    }
    void Update()
    {
        if(state)
        {
            if (beam != null)
            {
                Destroy(beam.laserObj);
            }
            beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserSize, distanceToCollider, distanceFromCollider, tinyDistance1, tinyDistance2);
        }
        else
        {
            if (beam != null)
            {
                Destroy(beam.laserObj);
            }
        }
    }
    public void toggle()
    {
        state = !state;
    }

    public void forceOn()
    {
        state = true;
    }
    public void forceOff()
    {
        state = false;
    }

    
}
