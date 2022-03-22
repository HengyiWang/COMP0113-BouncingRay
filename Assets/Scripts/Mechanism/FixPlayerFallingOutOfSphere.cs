using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPlayerFallingOutOfSphere : MonoBehaviour
{

    public GameObject sphere;
    public float maxPlayerOutOfSphereDistance = 3f;
    public float spawnOffset = 3f;
    private Vector3 sphereCenter;
    private float sphereRadius;


    // Start is called before the first frame update
    void Start()
    {
        if (sphere != null)
        {
            sphereCenter = sphere.GetComponent<Collider>().bounds.center;
            sphereRadius = sphere.GetComponent<Collider>().bounds.extents.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sphere == null)
        {
            return;
        }
        float distanceToSphereCenter = Vector3.Distance(sphereCenter, transform.position);
        float playerOutOfSphereDistance = distanceToSphereCenter - sphereRadius;
        if (playerOutOfSphereDistance > maxPlayerOutOfSphereDistance)
        {
            // need return to inside sphere
            Vector3 returnLocation = sphereCenter + ((transform.position - sphereCenter) * (sphereRadius - spawnOffset));
            transform.position = returnLocation;
        }
    }
}
