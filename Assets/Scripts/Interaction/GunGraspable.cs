using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class GunGraspable : MyFollowGraspable, IGraspable
{
    private Vector3 desiredForward;
    private Rigidbody rb;
    public new void Grasp(Hand controller)
    {
        // grasp if we have ownership
        base.Grasp(controller);
        var ownershipComp = GetComponent<NetworkedOwnership>();
        if (ownershipComp)
        {
            ownershipComp.Own(relinquish);
        }
        desiredForward = Vector3.RotateTowards(transform.forward, follow.forward, 180f, 0f);
        rb.isKinematic = true;
    }
    public void relinquish()
    {
        this.follow = null; 
    }

    public new void Release(Hand controller)
    {
        base.Release(controller);
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

    void Update()
    {
        // update position and rotation if this gun is grasped
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
