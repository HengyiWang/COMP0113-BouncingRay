using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// control the light of the shooting beam
public class ShootLighting : MonoBehaviour
{
    // Start is called before the first frame update
    public bool clicked = false;
    private GameObject spotLight;
    private NetworkedOwnership ownershipComp;
    void Start()
    {
        ownershipComp = GetComponent<NetworkedOwnership>();
        if (!ownershipComp)
        {
            Debug.LogError("ownership component is missing!");
        }
        spotLight = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (ownershipComp && ownershipComp.ownership)
        {
            clicked = Input.GetButton("Fire1");
        }
        bool grasped = GetComponent<MyFollowGraspable>().grasped;
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
