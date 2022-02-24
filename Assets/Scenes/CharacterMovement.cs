using UnityEngine;

/// <summary>
/// C# translation from http://answers.unity3d.com/questions/155907/basic-movement-walking-on-walls.html
/// Author: UA @aldonaletto 
/// </summary>

public class CharacterMovement : MonoBehaviour
{

    public float moveSpeed = 6; // move speed
    public float turnSpeed = 90; // turning speed (degrees/second)
    public float lerpSpeed = 10; // smoothing speed
    public float gravity = 9.8f; // gravity acceleration
    public float jumpStrength = 10; // vertical jump initial speed
    public float charFloatHeight = 0.2f;

    public BoxCollider charCollider;
    //public GameObject head;  // should contains main camera

    private Transform charTransform;
    private float distToGround;
    private Vector3 charNormal;

    private void Start()
    {

        charNormal = transform.up; // normal starts as character up direction
        charTransform = transform;
        distToGround = charCollider.size.y - charCollider.center.y + charFloatHeight;

        GetComponent<Rigidbody>().freezeRotation = true; // disable physics rotation
    }

    private void FixedUpdate()
    {
        // apply constant weight force according to character normal:
        GetComponent<Rigidbody>().AddForce(-gravity * GetComponent<Rigidbody>().mass * charNormal);
    }

    private void Update()
    {
        // update surface normal and isGrounded:
        RaycastHit hit;
        bool isGrounded;
        Vector3 currGroundNormal;
        
        Ray ray = new Ray(charTransform.position, -charNormal); // cast ray downwards
        if (Physics.Raycast(ray, out hit))
        { // use it to update myNormal and isGrounded
            isGrounded = hit.distance <= distToGround;
            currGroundNormal = hit.normal;
        }
        else
        {
            isGrounded = false;
            // assume usual ground normal to avoid "falling forever"
            currGroundNormal = Vector3.up;
        }

        if (isGrounded)
        {
            charNormal = Vector3.Lerp(charNormal, currGroundNormal, lerpSpeed * Time.deltaTime);
            // find forward direction with new myNormal:
            Vector3 charForward = Vector3.Cross(charTransform.right, charNormal);
            // align character to the new myNormal while keeping the forward direction:
            Quaternion targetRot = Quaternion.LookRotation(charForward, charNormal);
            charTransform.rotation = Quaternion.Lerp(charTransform.rotation, targetRot, lerpSpeed * Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                GetComponent<Rigidbody>().AddForce(charNormal * jumpStrength, ForceMode.Impulse);
            }
        }

        // move the character forth/back with Vertical axis
        charTransform.Translate(0, 0, Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        // movement code - turn left/right with Horizontal axis:
        charTransform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);
    }

}