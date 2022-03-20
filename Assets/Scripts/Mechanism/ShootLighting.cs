using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLighting : MonoBehaviour
{
    // Start is called before the first frame update
    public bool clicked = false;
    private GameObject spotLight;
    void Start()
    {
        spotLight = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        bool grasped = GetComponent<MyFollowGraspable>().grasped;
        clicked = Input.GetButton("Fire1");
        if (grasped && clicked)
        {
            spotLight.SetActive(true);
        }
        else
        {
            spotLight.SetActive(false);
        }
    }
}
