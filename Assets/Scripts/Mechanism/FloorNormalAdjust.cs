using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// adjust normal by using normal
public class FloorNormalAdjust : MonoBehaviour
{

    public float gravity = 9.8f; // gravity acceleration
    public float maxRaycastDistance = 100.0f;  // max distance to ground
    public int walkableLayerNumber = 8;
    public float adjustingHeight = 0.75f;
    public float normalAdjustLerpSpeed = 5;
    public float moveSpeed = 5.0f;

    private Vector3 charNormal;
    private Vector3 charForward;

    // Start is called before the first frame update
    void Start()
    {
        charNormal = transform.up;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().freezeRotation = true;
    }

    void Update()
    {
        adjustAvatarNormalByFloor();
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
        }
    }

    void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * charNormal);
    }
}
