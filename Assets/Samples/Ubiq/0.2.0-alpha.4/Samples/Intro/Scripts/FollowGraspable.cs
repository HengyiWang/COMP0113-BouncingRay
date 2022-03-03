using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;

namespace Ubiq.Samples
{
    public class FollowGraspable : MonoBehaviour, IGraspable
    {
        public bool grasped=false;
        private Vector3 localGrabPoint;
        private Quaternion localGrabRotation;
        private Quaternion grabHandRotation;
        private Transform follow;
        Vector3 desiredForward;
        private bool followRotation = false;
        public void Grasp(Hand controller)
        {
            grasped = true;
            var handTransform = controller.transform;
            localGrabPoint = handTransform.InverseTransformPoint(transform.position); //transform.InverseTransformPoint(handTransform.position);
            localGrabRotation = Quaternion.Inverse(handTransform.rotation) * transform.rotation;
            grabHandRotation = handTransform.rotation;
            follow = handTransform;

            desiredForward = Vector3.RotateTowards(transform.forward,follow.forward, 180f, 0f);
            
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
}
