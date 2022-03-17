using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    private NetworkedOwnership ownershipComp;
    public float x_init = 0f;
    public float y_init = 0f;
    public float z_init = 0f;
    public float dx = 0f;
    public float dy = 0.02f;
    public float dz = 0f;
    public float radius = 0.8f; 
    Vector3 oldPos;
    // Start is called before the first frame update
    void Start()
    {
        ownershipComp = GetComponent<NetworkedOwnership>();
        //radian = Random.Range(0, Mathf.PI);
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ownershipComp || (ownershipComp && ownershipComp.ownership))
        {
            x_init += dx;
            y_init += dy;
            z_init += dz;
            float x = Mathf.Sin(x_init) * radius;
            float y = Mathf.Sin(y_init) * radius;
            float z = Mathf.Sin(z_init) * radius;
            transform.position = oldPos + new Vector3(x, y, z);
        }
    }
}
