using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    //public Text ShowScore;
    public int Score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Score = GameObject.Find("ScoreBox").GetComponent<ScoreManager>().score;
        GetComponent<TextMesh>().text = "Score: " + Score.ToString();
    }
}
