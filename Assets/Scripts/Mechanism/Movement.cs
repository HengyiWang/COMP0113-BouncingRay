using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    
    public float gravity = 9.8f; // gravity acceleration
    public float maxRaycastDistance = 100.0f;  // max distance to ground
    public int walkableLayerNumber = 8;
    public float adjustingHeight = 0.5f;
    public float normalAdjustLerpSpeed = 10f;
    public float jumpStrength = 10;
    public float moveSpeed = 5.0f;
    public float turnSpeed = 100f;

    private Vector3 charNormal;


    // Start is called before the first frame update
    void Start()
    {
        charNormal = transform.up; // assumed character initial normal

        GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation
    }

    // Update is called once per frame
    void Update()
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
            Vector3 charForward = Vector3.Cross(transform.right, charNormal);
            // align character to the new myNormal while keeping the forward direction:
            Quaternion targetRot = Quaternion.LookRotation(charForward, charNormal);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRot, normalAdjustLerpSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(charNormal * jumpStrength, ForceMode.Impulse);
            }
        }

        // move the character forth/back with Vertical axis
        transform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        // movement code - turn left/right with Horizontal axis:
        transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
    }

    void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * charNormal);
    }
}
