using System.Collections;
using System.Collections.Generic;
using Ubiq.XR;
using UnityEngine;

public class MyFollowGraspable : MonoBehaviour, IGraspable
{
    public bool grasped = false;
    protected Transform follow;
    protected bool followRotation = false;

    protected Vector3 localGrabPoint;
    protected Quaternion localGrabRotation;
    private Quaternion grabHandRotation;
    
    public void Grasp(Hand controller)
    {
        grasped = true;
        var handTransform = controller.transform;
        localGrabPoint = handTransform.InverseTransformPoint(transform.position); //transform.InverseTransformPoint(handTransform.position);
        localGrabRotation = Quaternion.Inverse(handTransform.rotation) * transform.rotation;
        grabHandRotation = handTransform.rotation;
        follow = handTransform;


    }

    public void Release(Hand controller)
    {
        grasped = false;
        follow = null;
        followRotation = false;
    }

    private void Update()
    {
        if (follow)
        {
            transform.rotation = follow.rotation * localGrabRotation;
            transform.position = follow.TransformPoint(localGrabPoint);
        }
    }
}
