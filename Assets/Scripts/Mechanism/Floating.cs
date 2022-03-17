﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
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
        //radian = Random.Range(0, Mathf.PI);
        oldPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        x_init += dx;
        y_init += dy;
        z_init += dz;
        float x = Mathf.Cos(x_init) * radius;
        float y = Mathf.Cos(y_init) * radius;
        float z = Mathf.Cos(z_init) * radius;
        transform.position = oldPos + new Vector3(x, y, z);
    }
}
