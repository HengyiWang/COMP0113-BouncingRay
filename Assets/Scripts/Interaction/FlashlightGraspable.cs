using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;

public class FlashlightGraspable : MyFollowGraspable, IGraspable
{
    
    private Rigidbody rb;
    // Start is called before the first frame update
    public new void Grasp(Hand controller)
    {
        base.Grasp(controller);

        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public new void Release(Hand controller)
    {
        base.Release(controller);
        rb.useGravity = true;
        rb.isKinematic = false;
    }

    public void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (!rb)
        {
            Debug.LogError("No rigidbody in the flashlight!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.up = follow.forward;   
            transform.position = follow.TransformPoint(localGrabPoint);
        }

    }
}
