using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// destory gun hint when gun is grasped
public class GunHand : MonoBehaviour
{
    private GameObject all_laser_gun;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Grap any gun destroy the hand.
        all_laser_gun = GameObject.Find("All_Laser_Gun");
        foreach (Transform child in all_laser_gun.transform)
        {
            if (child.GetComponent<GunGraspable>().grasped)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
