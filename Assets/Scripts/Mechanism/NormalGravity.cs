using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalGravity : MonoBehaviour
{
    public float gravity = 9.8f; // gravity acceleration
    public float maxRaycastDistance = 100.0f;  // max distance to ground
    public int walkableLayerNumber = 8;

    public GameObject sphere;

    private Vector3 currNormal;

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
            Vector3 returnToSphereLocation = sphereCenter + (-currNormal * (sphereRadius - 10));
            transform.position = returnToSphereLocation;
        }

    }

    void FixedUpdate()
    {
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * currNormal);
    }
}
