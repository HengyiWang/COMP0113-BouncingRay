using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMirror : MonoBehaviour
{
    public bool hitted;
    public Vector3 hitPoint = new Vector3(0,0,0);
    private Vector3 lastHitPoint = new Vector3(0, 0, 0);
    private float ftime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    
    void Update()
    {
        // Sonar Shader Effect
        SimpleSonarShader_Parent parent = GetComponentInParent<SimpleSonarShader_Parent>();
        if (parent)
        {
            if (hitPoint != new Vector3(0, 0, 0))
            {
                if (hitPoint != lastHitPoint)
                {
                    parent.StartSonarRing(hitPoint, 10.0f);
                }
                else
                {
                    ftime += Time.deltaTime;
                    if (ftime >= 1f)
                    {
                        parent.StartSonarRing(hitPoint, 10.0f);
                        ftime = 0f;
                    }
                }

            }
        }
        
        lastHitPoint = hitPoint;
        hitPoint = new Vector3(0, 0, 0);
    }
}
