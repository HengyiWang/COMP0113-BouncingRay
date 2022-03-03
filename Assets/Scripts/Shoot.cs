using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Samples;

public class Shoot : MonoBehaviour
{
    public float range = 100f;

    private bool grasped;

    public int score = 0;

    float timer;
    float effectDisplayTime = 0.1f;//10 *Time.deltaTime;
    //int shootableMask;
    LineRenderer laser;
    List<Vector3> laserIndices;

    // Start is called before the first frame update
    void Start()
    {
        //shootableMask = LayerMask.GetMask("Shootable");
        // LineRenderer for the laser
        laser = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject.Find("Sci_fi_Pistol1").
        grasped = GetComponent<FollowGraspable>().grasped;
        // Update time for one frame
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && grasped)
        {
            BeginShoot();
            GetComponent<AudioSource>().Play();
        }
        else
        {
            laser.enabled = false;
            GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = 0;
        }
    }
    void BeginShoot()
    {
        timer = 0.0f;
        laser.enabled = true;
        laserIndices = new List<Vector3>();
        //laser.SetPosition(0,transform.position);
        CastRay(transform.position, transform.forward, laser, true);
        GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = score;
        score = 0;

        laserIndices = new List<Vector3>();
        CastRay(transform.position, transform.forward, laser, false);
    }


    void CastRay(Vector3 pos,Vector3 dir,LineRenderer laser,bool calScore)
    {
        laserIndices.Add(pos);
        Ray ray = new Ray(pos,dir);
        RaycastHit hitInfo;

        if(Physics.Raycast(ray, out hitInfo, range, 1))
        {
            // if hit something, then checkhit
            CheckHit(hitInfo, dir, laser, calScore);
            //laser.SetPosition(1, hitInfo.point);
        }
        else
        {
            // if not hit something, set the max range
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

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, bool calScore)
    {
        if (hitInfo.collider.gameObject.tag == "ReflectObject")
        {
            if (!hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitted && calScore)
            {
                score += 1;
                hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitted = true;
            }

            if (!calScore)
            {
                hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitted = false;

            }
            //Debug.Log("Hit reflect object");
            Vector3 pos = hitInfo.point;
            Vector3 dir = Vector3.Reflect(direction,hitInfo.normal);
            CastRay(pos, dir, laser, calScore);
        }
        else
        {
            laserIndices.Add(hitInfo.point);
            updateLaser();
        }
    }
}
