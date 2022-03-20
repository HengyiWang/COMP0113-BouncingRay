using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Samples;

public class DistributedShoot : MonoBehaviour
{
    public float range = 100f;
    public int colorID;
    private int number_of_all_gems;
    private bool grasped;
    public bool clicked = false;
    private NetworkedOwnership gunOwnership;
    public int score = 0;
    public Vector3 muzzlePosition = new Vector3(0.0f, 0.0f, 0.0f);
    //float timer;
    //float effectDisplayTime = 0.1f;//10 *Time.deltaTime;
    LineRenderer laserRenderer;
    List<Vector3> laserIndices;

    // Start is called before the first frame update
    void Start()
    {
        // LineRenderer for the laser
        laserRenderer = GetComponent<LineRenderer>();
        gunOwnership = GetComponent<NetworkedOwnership>();
        if (!gunOwnership)
        {
            Debug.LogError("ownership component is missing!");
        }

        number_of_all_gems = GameObject.Find("All_Gem").transform.childCount;
    }

    private bool OwnThisGun()
    {
        return gunOwnership && gunOwnership.ownership;
    }

    // Update is called once per frame
    void Update()
    {
        clicked = OwnThisGun() ? Input.GetButton("Fire1") : false;

        if (!clicked)
        {
            return;
        }

        grasped = GetComponent<MyFollowGraspable>().grasped;

        if (!grasped)
        {
            return;
        }

        // Update time for one frame
        //timer += Time.deltaTime;
        if (grasped && clicked)
        {
            BeginShoot();
            GetComponent<AudioSource>().Play();
        }
        else
        {
            laserRenderer.enabled = false;
            laserIndices = new List<Vector3>();
            updateLaser();
        }
        if (Input.GetButtonUp("Fire1"))//when release mouse left button
        {
            //GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = 0;
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
        //timer = 0.0f;
        laserRenderer.enabled = true;
        laserIndices = new List<Vector3>();
        //laser.SetPosition(0,transform.position);
        Vector3 startingPos = transform.TransformPoint(muzzlePosition);
        CastRay(startingPos, transform.forward, laserRenderer, true);
        //GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score = score;
        score = 0;

        laserIndices = new List<Vector3>();
        CastRay(startingPos, transform.forward, laserRenderer, false);
    }


    void CastRay(Vector3 pos, Vector3 dir, LineRenderer laser, bool calScore)
    {
        laserIndices.Add(pos);
        Ray ray = new Ray(pos, dir);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo, range, 1))
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
        laserRenderer.positionCount = laserIndices.Count;

        foreach (Vector3 idx in laserIndices)
        {
            laserRenderer.SetPosition(count, idx);
            count++;
        }
    }

    bool checkColorID(RaycastHit hitInfo)
    {
        int targetID = hitInfo.collider.gameObject.GetComponent<ScoreMirror>().colorID;
        return targetID == colorID || targetID == -1;
    }

    void CheckHit(RaycastHit hitInfo, Vector3 direction, LineRenderer laser, bool calScore)
    {
        if (hitInfo.collider.gameObject.tag == "ReflectObject" && checkColorID(hitInfo))
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
            Vector3 dir = Vector3.Reflect(direction, hitInfo.normal);
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
