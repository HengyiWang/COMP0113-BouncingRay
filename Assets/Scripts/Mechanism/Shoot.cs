using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Samples;

public class Shoot : MonoBehaviour
{
    public float range = 100f;
    private int number_of_all_gems;
    private bool grasped;
    public bool clicked = false;
    private NetworkedOwnership ownershipComp;
    public int score = 0;
    public Vector3 muzzlePosition = new Vector3(0.0f, 0.0f, 0.0f);
    float timer;
    float effectDisplayTime = 0.1f;//10 *Time.deltaTime;
    LineRenderer laser;
    List<Vector3> laserIndices;

    // Start is called before the first frame update
    void Start()
    {

        // LineRenderer for the laser
        laser = GetComponent<LineRenderer>();
        ownershipComp = GetComponent<NetworkedOwnership>();
        if (!ownershipComp)
        {
            Debug.LogError("ownership component is missing!");
        }

        number_of_all_gems = GameObject.Find("All_Gem").transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (ownershipComp && ownershipComp.ownership)
        {
            clicked = Input.GetButton("Fire1");
        }
        grasped = GetComponent<MyFollowGraspable>().grasped;
        // Update time for one frame
        timer += Time.deltaTime;
        if (grasped && clicked)
        {
            BeginShoot();
            GetComponent<AudioSource>().Play();
        }
        else
        {
            laser.enabled = false;
            laserIndices = new List<Vector3>();
            updateLaser();
        }
        if (Input.GetButtonUp("Fire1"))//when release mouse left button
        {
            GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = 0;
            GameObject.Find("Robot").GetComponent<Robots>().isHitted = false;

            clearMirrorSoundsTag();
        }
    }
    void clearMirrorSoundsTag()
    {
        var gemObjects = GameObject.FindGameObjectsWithTag("ReflectObject");

        foreach (var gemObject in gemObjects)
        {
            gemObject.GetComponent<ScoreMirror>().played = false;
        }
    }
    void BeginShoot()
    {
        timer = 0.0f;
        laser.enabled = true;
        laserIndices = new List<Vector3>();
        //laser.SetPosition(0,transform.position);
        Vector3 startingPos = transform.TransformPoint(muzzlePosition);
        CastRay(startingPos, transform.forward, laser, true);
        GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = score;
        score = 0;

        laserIndices = new List<Vector3>();
        CastRay(startingPos, transform.forward, laser, false);
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
            hitInfo.collider.gameObject.GetComponent<ScoreMirror>().playAudioSource();
            if (!hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitted && calScore)
            {
                score += 1;
                hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitted = true;
                hitInfo.collider.gameObject.GetComponent<ScoreMirror>().hitPoint = hitInfo.point;

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

        // Code for robot
        if (hitInfo.collider.gameObject.tag == "Robot")
        {
            hitInfo.collider.gameObject.GetComponent<Robots>().isHitted = true;

            if (score == number_of_all_gems && number_of_all_gems > 0)
            {
                hitInfo.collider.gameObject.GetComponent<Robots>().energy = true;
            }

        }
    }
}
