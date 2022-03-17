using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    public float dx=0f;
    public float dy=0f;
    public float dz=0f;
    private float x;
    private float y;
    private float z;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        x += dx;
        y += dy;
        z += dz;
        transform.localEulerAngles = new Vector3(x, y, z);
    }
}
