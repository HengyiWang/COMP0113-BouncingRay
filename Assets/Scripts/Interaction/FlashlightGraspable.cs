using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.XR;

public class FlashlightGraspable : GunGraspable
{
    
    // Update is called once per frame
    void Update()
    {
        if (follow)
        {
            transform.up = follow.forward;   
            transform.position = follow.TransformPoint(localGrabPoint);
        }

    }
}
