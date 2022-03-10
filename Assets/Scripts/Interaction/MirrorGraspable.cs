using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class MirrorGraspable : MyFollowGraspable, IGraspable
{

    public bool freedom=false;
    private Vector3 desiredForward;

    private Vector3 intialForward;

    public new void Grasp(Hand controller)
    {
        base.Grasp(controller);
        var ownershipComp = GetComponent<NetworkedOwnership>();
        if (ownershipComp)
        {
            ownershipComp.Own(relinquish);
        }
    }

    public void relinquish()
    {
        this.follow = null;
    }
    private Vector3 calDesiredForward()
    {
        Vector3 newForward;

        // Not free, then rotate around the up vector
        if (!freedom)
        {
            Vector3 mirrorToHand = follow.position - transform.position;
            newForward = Vector3.Normalize(Vector3.Cross(mirrorToHand, transform.up));

        }

        // free, then rotate to any angle around to the hand
        else
        {
            newForward = follow.forward;
        }

        // In case the cross product has oppsite direction
        if (Vector3.Dot(newForward, transform.forward) < 0)
        {
            newForward = -newForward;
        }

        // Lock. Make the process visually appealling
        if (Vector3.Dot(newForward, intialForward)<0)
        {
            newForward = transform.forward;
        }

        desiredForward = Vector3.RotateTowards(transform.forward, newForward, 90f, 0f);

        return desiredForward;
    }

    

    void Start()
    {
        intialForward = transform.forward;
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
