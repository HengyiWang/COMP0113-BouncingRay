using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class GunGraspable : MyFollowGraspable, IGraspable
{
    private Vector3 desiredForward;
    // Start is called before the first frame update

    public new void Grasp(Hand controller)
    {
        base.Grasp(controller);
        desiredForward = Vector3.RotateTowards(transform.forward, follow.forward, 180f, 0f);
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
