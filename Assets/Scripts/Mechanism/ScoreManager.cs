using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// score central, manage score
public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int total = 0;
    GameObject all_gun;
    // Start is called before the first frame update
    void Start()
    {
        total = GameObject.Find("All_Gem").transform.childCount;
        all_gun = GameObject.Find("All_Laser_Gun");
    }

    // Update is called once per frame
    void Update()
    {
        int score_of_all_gun = 0;
        foreach(Transform child in all_gun.transform)
        {
            score_of_all_gun += child.GetComponent<Shoot>().score_this_gun;
            child.GetComponent<Shoot>().score_this_gun = 0;
        }
        score = score_of_all_gun;
        if (score == total && total > 0)
        {
            GameObject.Find("Robot").GetComponent<Robots>().energy = true;
        }
    }
}
