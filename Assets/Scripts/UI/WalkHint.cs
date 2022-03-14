using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkHint : MonoBehaviour
{
    // usage: attach to a character and change the destination
    public Vector3 destination;

    public GameObject pathHint;
    //public GameObject lastHint;
    public float initialOffset = 5;
    public float finalOffset = 1;
    public float gap = 1.5f;

    private List<GameObject> existingHints;

    float pathHintLength;
    
    // Start is called before the first frame update
    void Start()
    {
        existingHints = new List<GameObject>();
    }


    // Update is called once per frame
    void Update()
    {
        destoryAllHints();
        float availableDistance = Vector3.Distance(transform.position, destination);
        //Debug.Log(nHints);
        float occupiedDistance = initialOffset;
        while (occupiedDistance < availableDistance)
        {
            Vector3 hintPosition = Vector3.Lerp(transform.position, destination, (occupiedDistance - finalOffset) / availableDistance);
            //Debug.Log(i.ToString() + ": " + hintPosition.ToString() + ", " + (i / nHints).ToString());
            GameObject obj = Instantiate(pathHint, hintPosition, Quaternion.identity);
            obj.transform.up = destination - transform.position;
            occupiedDistance += obj.GetComponentInChildren<Renderer>().bounds.size.z + gap;
            existingHints.Add(obj);
        }
    }

    void destoryAllHints()
    {
        foreach (GameObject gameObject in existingHints)
        {
            Destroy(gameObject);
        }
        existingHints = new List<GameObject>();
    }
}
