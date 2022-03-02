﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float range = 100f;

    float timer;
    float effectDisplayTime = 0.5f;
    //int shootableMask;
    LineRenderer laser;
    List<Vector3> laserIndices;

    // Start is called before the first frame update
    void Start()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            BeginShoot();
        }
        if(timer >= effectDisplayTime)
        {
            laser.enabled = false;
        }
    }
    void BeginShoot()
    {
        timer = 0.0f;
        laser.enabled = true;
        laserIndices = new List<Vector3>();
        //laser.SetPosition(0,transform.position);
        CastRay(transform.position, transform.forward, laser);
    }

    void CastRay(Vector3 pos,Vector3 dir,LineRenderer laser)
    {
        laserIndices.Add(pos);
        Ray ray = new Ray(pos,dir);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, range, 1))
        {
            CheckHit(hitInfo, dir, laser);
            //laser.SetPosition(1, hitInfo.point);
        }
        else
        {
            //laser.SetPosition(1, ray.origin+ ray.direction*range);
            laserIndices.Add(ray.GetPoint(range));
            updateLaser();
        }
    }

    void updateLaser()
    {
        int count = 0;
        laser.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laser.SetPosition(count,idx);
            count++;
        }
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser)
    {
        if (hitInfo.collider.gameObject.tag == "ReflectObject")
        {
            //Debug.Log("Hit reflect object");
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction,hitInfo.normal);
            CastRay(pos, dir, laser);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            updateLaser();
        }
    }
}