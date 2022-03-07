using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class MirrorGraspable : MyFollowGraspable, IGraspable
{
    private Vector3 desiredForward;

    private Vector3 calDesiredForward()
    {
        Vector3 mirrorToHand = follow.position - transform.position;
        Vector3 newForward = Vector3.Normalize(Vector3.Cross(mirrorToHand, transform.up));

        if (Vector3.Dot(newForward, transform.forward) < 0)
        {
            newForward = -newForward;
        }

        desiredForward = Vector3.RotateTowards(transform.forward, newForward, 90f, 0f);

        return desiredForward;
    }

    void Update()
    {
        if (follow)
        {
            desiredForward = calDesiredForward();
            transform.rotation = Quaternion.LookRotation(desiredForward);
        }
    }
}
