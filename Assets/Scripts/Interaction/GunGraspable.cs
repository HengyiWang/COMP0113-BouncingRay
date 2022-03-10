using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class GunGraspable : MyFollowGraspable, IGraspable
{
    private Vector3 desiredForward;
    // Start is called before the first frame update
    private Rigidbody rb;
    public new void Grasp(Hand controller)
    {
        base.Grasp(controller);
        var ownershipComp = GetComponent<NetworkedOwnership>();
        if (ownershipComp)
        {
            ownershipComp.Own(relinquish);
        }
        desiredForward = Vector3.RotateTowards(transform.forward, follow.forward, 180f, 0f);
        rb.useGravity = false;
        rb.isKinematic = true;
    }
    public void relinquish()
    {
        this.follow = null; 
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
            Debug.LogError("No rigidbody in the gun!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            if (!followRotation)
            {
                transform.rotation = Quaternion.LookRotation(desiredForward);
                localGrabRotation = Quaternion.Inverse(follow.rotation) * transform.rotation;
                followRotation = true;
            }
            else
            {
                transform.rotation = follow.rotation * localGrabRotation;
            }
            transform.position = follow.TransformPoint(localGrabPoint);
        }

    }
}
