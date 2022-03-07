using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public float gravity = 9.8f; // gravity acceleration
    private Vector3 charNormal;

    // Start is called before the first frame update
    void Start()
    {
        charNormal = transform.up; // normal starts as character up direction
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * charNormal);
    }
}
