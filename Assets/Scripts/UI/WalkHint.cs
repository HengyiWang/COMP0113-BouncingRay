using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WalkHint : MonoBehaviour
{
    // usage: attach to a character and change the destination
    public GameObject sphere;
    public List<Transform> destinations;
    public GameObject pathHint;
    public float initialOffset = 5f;
    public float finalOffset = 1f;
    public float gap = 1.5f;
    public float pathHintGroundOffset = 0.3f;
    public float lastHintGroundOffset = 1.5f;
    public float lastHintRotateSpeed = 180f;
    public float reachThreshold = 5.0f;
    public Vector3 hintScale = Vector3.one * 0.05f;
    public Vector3 hintRotation = new Vector3(180, 0, 0);

    private Dictionary<Transform, List<GameObject>> existingHints;
    private Vector3 sphereCenter;
    private float sphereRadius;

    // Start is called before the first frame update
    void Start()
    {
        existingHints = new Dictionary<Transform, List<GameObject>>();
        MeshCollider collider = sphere.GetComponent<MeshCollider>();
        sphereCenter = collider.bounds.center;
        sphereRadius = collider.bounds.extents.x;
    }

    // Update is called once per frame
    void Update()
    {
        destoryAllPathHints();
        if (destinations.Count == 0)
        {
            return;
        }

        Vector3 start = findClosestPointOnSphere(transform.position);


        foreach (Transform destination in destinations.ToList())  // iterate over a copy because destination may change
        {

            Vector3 end = findClosestPointOnSphere(destination.position);

            if (Vector3.Distance(start, end) < reachThreshold)
            {
                destinations.Remove(destination);
                continue;
            }

            if (!existingHints.ContainsKey(destination))
            {
                existingHints.Add(destination, new List<GameObject>());
            }

            float availableDistance = distanceBetweenTwoPointsOnSphere(start, end);
            Vector3 lastHintPosition = start;
            float occupiedDistance = initialOffset;

            while (occupiedDistance < availableDistance)
            {
                Vector3 hintPosition = Vector3.Slerp(start, end, occupiedDistance / (availableDistance + finalOffset));
                GameObject pathHintInstance = Instantiate(pathHint, hintPosition, Quaternion.identity);
                pathHintInstance.transform.rotation = Quaternion.LookRotation(hintPosition - sphereCenter, hintPosition - lastHintPosition);
                pathHintInstance.transform.Translate(0, 0, -pathHintGroundOffset);
                pathHintInstance.transform.localScale = hintScale;
                pathHintInstance.transform.Rotate(hintRotation);
                occupiedDistance += distanceBetweenTwoPointsOnSphere(hintPosition, lastHintPosition) + gap;
                existingHints[destination].Add(pathHintInstance);
                lastHintPosition = hintPosition;
            }

            GameObject lastHintInstance = Instantiate(pathHint, end, Quaternion.identity);
            lastHintInstance.transform.up = end - sphereCenter;
            lastHintInstance.transform.Translate(0, -lastHintGroundOffset, 0);
            lastHintInstance.transform.Rotate(Vector3.up, Time.time * lastHintRotateSpeed);
            lastHintInstance.transform.localScale = hintScale;
            lastHintInstance.transform.Rotate(hintRotation);
            existingHints[destination].Add(lastHintInstance);
        }

       
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

    void destoryPathHints(Transform key)
    {
        List<GameObject> hintsForThisKey;

        if (existingHints.TryGetValue(key, out hintsForThisKey))
        {
            foreach (GameObject gameObject in hintsForThisKey)
            {
                Destroy(gameObject);
            }
            existingHints.Remove(key);
        }
    }

    void destoryAllPathHints()
    {
        foreach (Transform key in existingHints.Keys.ToArray())
        {
            destoryPathHints(key);
        }
    }
}
