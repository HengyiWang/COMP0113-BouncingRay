using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ubiq.XR;

public class XRMovement : MonoBehaviour
{

    [NonSerialized]
    public HandController[] handControllers;

    public float joystickFlySpeed = 5f;
    public float joystickDeadzone = 0.1f;

    private void Awake()
    {
        handControllers = GetComponentsInChildren<HandController>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnTeleport(Vector3 position)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var item in handControllers)
        {
            if (item.Right)
            {
                if (item.JoystickSwipe.Trigger)
                {
                    transform.RotateAround(transform.position, Vector3.up, 45f * Mathf.Sign(item.JoystickSwipe.Value));
                }
            }
            else if (item.Left)
            {
                var dir = item.Joystick.normalized;
                transform.position += new Vector3(dir.x, 0, dir.y) * joystickFlySpeed * Time.deltaTime;
            }
        }

        //var headProjectionXZ = transform.InverseTransformPoint(headCamera.transform.position);
        //headProjectionXZ.y = 0;
        //userLocalPosition.x += (headProjectionXZ.x - userLocalPosition.x) * Time.deltaTime * cameraRubberBand.Evaluate(Mathf.Abs(headProjectionXZ.x - userLocalPosition.x));
        //userLocalPosition.z += (headProjectionXZ.z - userLocalPosition.z) * Time.deltaTime * cameraRubberBand.Evaluate(Mathf.Abs(headProjectionXZ.z - userLocalPosition.z));
        //userLocalPosition.y = 0;
    }
}
