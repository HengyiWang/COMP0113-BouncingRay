using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Ubiq.XR;
using Ubiq.Samples;

public class Movement : MonoBehaviour
{

    public float gravity = 9.8f; // gravity acceleration
    public float maxRaycastDistance = 100.0f;  // max distance to ground
    public int walkableLayerNumber = 8;
    public float adjustingHeight = 0.75f;
    public float normalAdjustLerpSpeed = 5;  // speed of adjusting character normal
    public float jumpStrength = 10;
    public float moveSpeed = 5.0f;
    public float cameraRotateSpeed = 3f;
    public int maxVerticalViewOffsetAngle = 45;
    public bool VR = true;  // if we are using VR

    private Vector3 charNormal;
    private Vector3 charForward;

    [NonSerialized]
    public HandController[] handControllers;

    private Transform cameraOffsetTransform;

    // Start is called before the first frame update
    void Start()
    {
        charNormal = transform.up; // assumed character initial normal
        GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation

        cameraOffsetTransform = transform.Find("Camera Offset");

        // check if we are using VR
        VR = VR && UnityEngine.XR.XRSettings.isDeviceActive;
        if (VR)
        {
            handControllers = GetComponentsInChildren<HandController>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        adjustAvatarNormalByFloor();
        moveAvatarOnKeyPressed();
        rotateCameraOnRightClick();
        moveAvatarInVR();
    }

    void moveAvatarInVR()
    {
        if (!VR)
        {
            return;
        }

        // update similar to ubiq, but this works even if character is not upright
        foreach (var item in handControllers)
        {
            if (item.Right)
            {
                if (item.JoystickSwipe.Value != 0)
                {
                    transform.RotateAround(transform.position, transform.up, cameraRotateSpeed * Mathf.Sign(item.JoystickSwipe.Value));
                }
            }
            else if (item.Left)
            {
                var dir = item.Joystick.normalized;
                transform.Translate(new Vector3(dir.x, 0, dir.y) * moveSpeed * Time.deltaTime);
            }
        }
    }

    void adjustAvatarNormalByFloor()
    {
        RaycastHit hit;
        bool adjust;  // we do not adjust if the play is jumping
        Vector3 currGroundNormal;

        int walkableMask = 1 << walkableLayerNumber;

        Ray ray = new Ray(transform.position, -charNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit, maxRaycastDistance, walkableMask))
        { // use it to update myNormal and isGrounded

            adjust = hit.distance <= adjustingHeight;
            currGroundNormal = hit.normal;
        }
        else
        {
            adjust = false;
            // assume usual ground normal to avoid "falling forever"
            currGroundNormal = Vector3.up;
        }

        if (adjust)
        {
            charNormal = Vector3.Lerp(charNormal, currGroundNormal, normalAdjustLerpSpeed * Time.deltaTime);
            // find forward direction with new myNormal:
            charForward = Vector3.Cross(transform.right, charNormal);
            // align character to the new myNormal while keeping the forward direction:
            Quaternion targetRot = Quaternion.LookRotation(charForward, charNormal);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, normalAdjustLerpSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(charNormal * jumpStrength, ForceMode.Impulse);
            }
        }
    }

    void moveAvatarOnKeyPressed()
    {
        // move accordingly, same as ubiq implementation
        Vector3 movement = new Vector3(0f, 0f, 0f);
        if (Input.GetKey(KeyCode.A))
        {
            movement += new Vector3(-1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(1f, 0f, 0f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            movement += new Vector3(0f, 0f, 1f);
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement += new Vector3(0f, 0f, -1f);
        }
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void rotateCameraOnRightClick()
    {
        if (Input.GetMouseButton(1))
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * cameraRotateSpeed, 0);
            float angle = Vector3.SignedAngle(charForward, cameraOffsetTransform.forward, transform.right);
            if (Math.Abs(angle) < maxVerticalViewOffsetAngle || Math.Sign(angle) == Math.Sign(Input.GetAxis("Mouse Y")))
            {
                cameraOffsetTransform.Rotate(-Input.GetAxis("Mouse Y") * cameraRotateSpeed, 0, 0);
            }
        }

    }

    void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * charNormal);
    }
}
