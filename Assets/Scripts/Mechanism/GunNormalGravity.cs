using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// apply gravity to gun, which does not care about normal, but require sphere center
public class GunNormalGravity : MonoBehaviour
{
    public float gravity = 9.8f; // gravity acceleration
    public float maxRaycastDistance = 100.0f;  // max distance to ground
    public int walkableLayerNumber = 8;

    private Vector3 currNormal;

    public GameObject sphere;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sphereCenter = sphere.GetComponent<Collider>().bounds.center;
        float sphereRadius = sphere.GetComponent<Collider>().bounds.extents.y;
        currNormal = sphereCenter - transform.position;
        currNormal = currNormal.normalized;
        float distanceToCenter = Vector3.Distance(sphereCenter, transform.position);
        
        if (distanceToCenter > sphereRadius)
        {
            Vector3 spawnLocation = sphereCenter + (-currNormal * (sphereRadius - 3));
            transform.position = spawnLocation;
        }
    }

    void FixedUpdate()
    {
        if (!GetComponent<GunGraspable>().grasped)
        {
            GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * currNormal);
        }
    }
}
