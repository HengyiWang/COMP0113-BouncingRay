using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour
{
    private float lastTurningTime;
    private bool needTurning;
    private float remainingRotateAngle;

    public float speed = 16f;
    public float rotateSpeed = 60f;
    public float turningFrequencyInSeconds = 7;
    public float closeToWallDistance = 1f;

    // Start is called before the first frame update
    void Start()
    {
        lastTurningTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        float timeElaspedInSeconds = Time.time - lastTurningTime;
        if (!needTurning && timeElaspedInSeconds >= turningFrequencyInSeconds)
        {
            setTurning();
        }

        if (!needTurning && closeToWall())
        {
            setTurning();
        }

        if (needTurning)
        {
            float turnDirection = Mathf.Sign(remainingRotateAngle);
            float rotateAngle = Time.deltaTime * rotateSpeed * turnDirection;
            transform.Rotate(0, rotateAngle, 0);
            remainingRotateAngle -= rotateAngle;
            //Debug.Log("rotate: " + rotateAngle);
            if (turnDirection != Mathf.Sign(remainingRotateAngle))
            {
                needTurning = false;
            }
        }
        else
        {
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.name == "Level0sphere")
        {
            setTurning();
        }
    }

    bool closeToWall()
    {
        Ray ray = new Ray(GetComponent<Collider>().bounds.center, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.name == "Level0sphere" && hitInfo.distance < closeToWallDistance)
            {
                return true;
            }
        }
        return false;
    }

    void setTurning()
    {
        needTurning = true;
        remainingRotateAngle = Random.Range(45, 180) * (Random.value > 0.5 ? -1 : 1);
        lastTurningTime = Time.time;
    }
}
