using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkHint : MonoBehaviour
{
    // usage: attach to a character and change the destination
    public GameObject sphere;
    public Transform destination;
    public GameObject pathHint;
    public float initialOffset = 5f;
    public float finalOffset = 1f;
    public float gap = 1.5f;
    public float pathHintGroundOffset = 0.3f;
    public float lastHintGroundOffset = 1.5f;
    public float lastHintRotateSpeed = 180f;

    private List<GameObject> existingHints;
    private Vector3 sphereCenter;
    private float sphereRadius;

    // Start is called before the first frame update
    void Start()
    {
        
        existingHints = new List<GameObject>();
        MeshCollider collider = sphere.GetComponent<MeshCollider>();
        sphereCenter = collider.bounds.center;
        sphereRadius = collider.bounds.extents.x;
    }


    // Update is called once per frame
    void Update()
    {
        destoryAllPathHints();
        Vector3 end = findClosestPointOnSphere(destination.position);
        Vector3 start = findClosestPointOnSphere(transform.position);
        float availableDistance = distanceBetweenTwoPointsOnSphere(start, end);
        Vector3 lastHintPosition = start;
        float occupiedDistance = initialOffset;

        while (occupiedDistance < availableDistance)
        {
            Vector3 hintPosition = Vector3.Slerp(start, end, occupiedDistance / (availableDistance + finalOffset));
            //Vector3 hintPosition = findClosestPointOnSphere((sphereCenter + hintDirection) * sphereRadius);
            GameObject pathHintInstance = Instantiate(pathHint, hintPosition, Quaternion.identity);
            pathHintInstance.transform.rotation = Quaternion.LookRotation(hintPosition - sphereCenter, hintPosition - lastHintPosition);
            pathHintInstance.transform.Translate(0, 0, -pathHintGroundOffset);
            occupiedDistance += distanceBetweenTwoPointsOnSphere(hintPosition, lastHintPosition) + gap;
            existingHints.Add(pathHintInstance);
            lastHintPosition = hintPosition;
        }

        GameObject lastHintInstance = Instantiate(pathHint, end, Quaternion.identity);
        lastHintInstance.transform.up = end - sphereCenter;
        lastHintInstance.transform.Translate(0, -lastHintGroundOffset, 0);
        lastHintInstance.transform.Rotate(Vector3.up, Time.time * lastHintRotateSpeed);
        existingHints.Add(lastHintInstance);
    }

    Vector3 findClosestPointOnSphere(Vector3 position)
    {
        Vector3 direction = (position - sphereCenter).normalized;
        return sphereCenter + direction * sphereRadius;
    }

    float distanceBetweenTwoPointsOnSphere(Vector3 a, Vector3 b)
    {
        Vector3 toA = a - sphereCenter;
        Vector3 toB = b - sphereCenter;
        float angleBetween = Vector3.Angle(toA, toB);

        return 2 * Mathf.PI * sphereRadius * (angleBetween / 360);
    }

    void destoryAllPathHints()
    {
        foreach (GameObject gameObject in existingHints)
        {
            Destroy(gameObject);
        }
        existingHints = new List<GameObject>();
    }
}
