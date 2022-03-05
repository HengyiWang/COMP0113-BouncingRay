using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;
using System;

public class XRCharacterMovement : MonoBehaviour
{

    public Camera headCamera;
    [NonSerialized]
    public HandController[] handControllers;

    public float joystickDeadzone = 0.1f;
    public float joystickFlySpeed = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        handControllers = GetComponentsInChildren<HandController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update the foot position. This is done by pulling the feet using a rubber band.
        // Decoupling the feet in this way allows the user to do things like lean over edges, when the ground check is enabled.
        // This can be effectively disabled by setting the animation curve to a constant high value.

        foreach (var item in handControllers)
        {
            if (item.Right)
            {
                if (item.JoystickSwipe.Trigger)
                {
                    transform.RotateAround(headCamera.transform.position, Vector3.up, 45f * Mathf.Sign(item.JoystickSwipe.Value));
                }
            }
            else if (item.Left)
            {
                var dir = item.Joystick.normalized;
                var mag = item.Joystick.magnitude;
                if (mag > joystickDeadzone)
                {
                    //var speedMultiplier = Mathf.InverseLerp(joystickDeadzone, 1.0f, mag);
                    var worldDir = headCamera.transform.TransformDirection(dir.x, 0, dir.y);
                    worldDir.y = 0;
                    var distance = (joystickFlySpeed * Time.deltaTime);
                    transform.position += distance * worldDir.normalized;
                }
            }
        }

        //var headProjectionXZ = transform.InverseTransformPoint(headCamera.transform.position);
        //headProjectionXZ.y = 0;
        //userLocalPosition.x += (headProjectionXZ.x - userLocalPosition.x) * Time.deltaTime * cameraRubberBand.Evaluate(Mathf.Abs(headProjectionXZ.x - userLocalPosition.x));
        //userLocalPosition.z += (headProjectionXZ.z - userLocalPosition.z) * Time.deltaTime * cameraRubberBand.Evaluate(Mathf.Abs(headProjectionXZ.z - userLocalPosition.z));
        //userLocalPosition.y = 0;
    }
}
