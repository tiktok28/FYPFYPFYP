using System.Collections;
using System.Collections.Generic;
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
    void Update()
    {
        if (beam != null)
        {
            Destroy(beam.laserObj);
        }
        beam = new LaserBeam(gameObject.transform.position, gameObject.transform.right, material, laserSize, distanceToCollider, distanceFromCollider, tinyDistance1, tinyDistance2);
    }

    
}
