using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// destory mirror hand when grasped
public class MirrorHand : MonoBehaviour
{
    private GameObject all_gem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        all_gem = GameObject.Find("All_Gem");
        foreach (Transform child in all_gem.transform)
        {
            if (child.GetComponent<MirrorGraspable>().grasped)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }
}
