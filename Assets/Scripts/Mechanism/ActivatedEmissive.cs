using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// set the lighting for gun light
public class ActivatedEmissive : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxEmissive;
    public ScoreMirror scoreMirrorComp;
    public Material m;

    private void Awake()
    {
        this.maxEmissive = 1.0f;
        this.scoreMirrorComp = GetComponent<ScoreMirror>();
        this.m = GetComponent<Renderer>().material;
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreMirrorComp && scoreMirrorComp.played)
        {
            if (m)
            {
                m.SetFloat("_Emission", maxEmissive * Mathf.Abs(Mathf.Sin(Time.time)));
            }
        }
    }
}
