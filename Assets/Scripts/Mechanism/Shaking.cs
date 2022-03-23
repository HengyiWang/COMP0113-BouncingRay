using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// rotate hand according 
public class Shaking : MonoBehaviour
{
    public float dx=0f;
    public float dy=0f;
    public float dz=0f;
    public float speed=0.05f;
    private float x=0f;
    private float y=0f;
    private float z=0f;
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

        //transform.localEulerAngles = new Vector3(Mathf.Sin(x), Mathf.Sin(y), Mathf.Sin(z));
        transform.Rotate(Mathf.Sin(speed * x), Mathf.Sin(speed * y), Mathf.Sin(speed * z));
    }
}
