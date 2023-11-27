using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam
{
    Vector3 pos, dir;

    public GameObject laserObj;
    public LineRenderer laser;
    public List<Vector3> laserIndices = new List<Vector3>();
    public float distanceToCollider;
    public float distanceFromCollider;
    public float tinyDistance1;
    public float tinyDistance2;
    
    Dictionary<string, float> refractiveMaterials = new Dictionary<string, float>()
    {
        {"Air", 1.0f},
        {"Glass", 1.5f}
    };
    public LaserBeam(Vector3 pos, Vector3 dir, Material material, float size, float distanceToCollider, float distanceFromCollider, float tinyDistance1, float tinyDistance2)
    {
        this.laser = new LineRenderer();
        this.laserObj = new GameObject();
        this.laserObj.name = "Laser Beam";
        this.pos = pos;
        this.dir = dir;

        this.laser = this.laserObj.AddComponent(typeof(LineRenderer)) as LineRenderer;
        this.laser.startWidth = size;
        this.laser.endWidth = size;
        this.laser.material = material;
        this.laser.startColor = Color.green;
        this.laser.endColor = Color.green;
        this.distanceToCollider = distanceToCollider;
        this.distanceFromCollider = distanceFromCollider;
        this.tinyDistance1 = tinyDistance1;
        this.tinyDistance2 = tinyDistance2;

        CastRay(pos, dir, laser);
    }

    public void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser)
    {
        laserIndices.Add(pos);

        Ray ray = new Ray(pos, dir);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 30, 1))
        {
            CheckHit(hit, dir, laser);
        }
        else
        {
            laserIndices.Add(ray.GetPoint(30));
            UpdateLaser();
        }
    }

    void UpdateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count, idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if(hitInfo.collider.gameObject.tag == "Mirror")
        {
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);

            CastRay(pos, dir, laser);
        }
        // else if(hitInfo.collider.gameObject.tag == "Prism")
        // {
        //     Debug.Log("Incident angle=" + Vector3.Angle(direction, -hitInfo.normal));
        //     Vector3 pos = hitInfo.point;
        //     float refractiveIndex = hitInfo.collider.gameObject.GetComponent<PrismVariables>().refractiveIndex;
        //     Vector3 dir = Refract(direction, hitInfo.normal, refractiveIndex);
        //     Debug.Log("Refracted angle=" + Vector3.Angle(dir, -hitInfo.normal));
        //     CastRay(pos, dir, laser);
        // }
        else if(hitInfo.collider.gameObject.tag == "Prism")
        {
            Vector3 pos = hitInfo.point;
            laserIndices.Add(pos);

            Vector3 newPos1 = new Vector3(Mathf.Abs(direction.x)/ (direction.x + tinyDistance1) * tinyDistance2 + pos.x, Mathf.Abs(direction.y)/ (direction.y + tinyDistance1) * tinyDistance2 + pos.y, Mathf.Abs(direction.z)/ (direction.z + tinyDistance1) * tinyDistance2 + pos.z);

            float n1 = refractiveMaterials["Air"];
            // float n2 = refractiveMaterials["Glass"];
            float n2 = hitInfo.collider.gameObject.GetComponent<PrismVariables>().refractiveIndex;
            Vector3 norm = hitInfo.normal;
            Vector3 incident = direction;

            Vector3 refractedVector = Refract(n1, n2, norm, incident);

            Ray ray1 = new Ray(newPos1, refractedVector);
            Vector3 newRayStartPos = ray1.GetPoint(distanceToCollider);

            Ray ray2 = new Ray(newRayStartPos, -refractedVector);
            RaycastHit hit2;

            Debug.Log("Incident angle=" + Vector3.Angle(direction, -hitInfo.normal));
            Debug.Log("Refracted angle=" + Vector3.Angle(refractedVector, -hitInfo.normal));

            if(Physics.Raycast(ray2, out hit2, distanceFromCollider, 1))
            {
                laserIndices.Add(hit2.point);
            }

            UpdateLaser();

            Vector3 refractedVector2 = Refract(n2, n1, -hit2.normal, refractedVector);
            CastRay(hit2.point, refractedVector2, laser);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            UpdateLaser();
        }
    }

    Vector3 Refract(float n1, float n2, Vector3 norm, Vector3 incident)
    {
        incident.Normalize();

        Vector3 refractedVector = (n1/n2 * Vector3.Cross(norm, Vector3.Cross(-norm, incident)) - norm * Mathf.Sqrt(1 - Vector3.Dot(Vector3.Cross(norm, incident) * (n1/n2 * n1/n2), Vector3.Cross(norm, incident)))).normalized;

        return refractedVector;
    }

    // Vector3 Refract(Vector3 dir, Vector3 normal, float refractiveIndex)
    // {
    //     float cosTheta1 = Vector3.Dot(-dir, normal);
    //     Debug.Log("cosTheta1=" + cosTheta1);
    //     float sinTheta1 = Mathf.Sqrt(1.0f - cosTheta1 * cosTheta1);
    //     Debug.Log("sinTheta1=" + sinTheta1);
    //     float sinTheta2 = (1.0f / refractiveIndex) * sinTheta1;
    //     Debug.Log("sinTheta2=" + sinTheta2);
    //     float cosTheta2 = Mathf.Sqrt(1.0f - sinTheta2 * sinTheta2);
    //     Debug.Log("cosTheta2=" + cosTheta2);
    //     Vector3 refract = (1.0f / refractiveIndex) * dir + ((1.0f / refractiveIndex) * cosTheta1 - cosTheta2) * normal;
    //     return refract;
    // }
}
