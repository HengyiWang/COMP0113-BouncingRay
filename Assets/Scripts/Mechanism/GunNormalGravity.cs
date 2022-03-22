using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        currNormal = sphere.GetComponent<Collider>().bounds.center - transform.position;
        currNormal = currNormal.normalized;
    }

    void FixedUpdate()
    {
        if (!GetComponent<GunGraspable>().grasped)
        {
            GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * currNormal);
        }
    }
}
