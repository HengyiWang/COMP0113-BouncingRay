using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;
    public int total = 0;
    // Start is called before the first frame update
    void Start()
    {
        total = GameObject.Find("All_Gem").transform.childCount;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
