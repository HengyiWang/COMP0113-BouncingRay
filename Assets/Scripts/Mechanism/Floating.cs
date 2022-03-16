using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float radian = 0; 
    public float radianChangeRate = 0.02f; 
    public float radius = 0.8f; 
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        radian = Random.Range(0, Mathf.PI);
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        radian += radianChangeRate; 
        float dy = Mathf.Cos(radian) * radius; 
        transform.position = oldPos + new Vector3(0, dy, 0);
    }
}
