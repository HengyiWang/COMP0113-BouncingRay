//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Shaking : MonoBehaviour
//{
//    public float radian = 0;
//    public float radianChangeRate = 0.02f;
//    float radius = 0.8f;
//    Vector3 oldRot;
//    // Start is called before the first frame update
//    void Start()
//    {
//        radian = Random.Range(0, Mathf.PI);
//        oldRot = transform.rotation;
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        radian += radianChangeRate;
//        float dy = Mathf.Cos(radian) * radius;
//        transform.rotation = oldRot + new Vector3(0, dy, 0);
//    }
//}
