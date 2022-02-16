using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private Vector3 m_camRot;
    private Transform m_camTransform;//摄像机Transform
    private Transform m_transform;//摄像机父物体Transform
    public float m_movSpeed = 10;//移动系数
    public float m_rotateSpeed = 1;//旋转系数

    // Start is called before the first frame update
    void Start()
    {
        m_camTransform = Camera.main.transform;
        m_transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //获取鼠标移动距离
            float rh = Input.GetAxis("Mouse X");
            float rv = Input.GetAxis("Mouse Y");

            // 旋转摄像机
            m_camRot.x -= rv * m_rotateSpeed;
            m_camRot.y += rh * m_rotateSpeed;

        }
        m_camTransform.eulerAngles = m_camRot;
        Vector3 camrot = m_camTransform.eulerAngles;
        camrot.x = 0; camrot.z = 0;
        m_transform.eulerAngles = camrot;
        float xm = 0, ym = 0, zm = 0;
        if (Input.GetKey(KeyCode.W))
        {
            zm += m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S))//向后移动
        {
            zm -= m_movSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))//向左移动
        {
            xm -= m_movSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.D))//向右移动
        {
            xm += m_movSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Space) && m_transform.position.y <= 3)
        {
            ym += m_movSpeed * Time.deltaTime;
        }
        //if (Input.GetKey(KeyCode.F) && m_transform.position.y >= 1) //向下
        //{
        //    ym -= m_movSpeed * Time.deltaTime;
        //}
        
        m_transform.Translate(new Vector3(xm, ym, zm), Space.Self);
    }
}
