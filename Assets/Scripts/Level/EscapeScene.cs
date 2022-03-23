using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeScene : MonoBehaviour
{
    private static EscapeScene singleton;

    // Start is called before the first frame update
    void Start()
    {
        if (singleton == null)
        {
            DontDestroyOnLoad(this);
            singleton = this;
        } else if (singleton != this)
        {
            Destroy(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if pressed escape, return to home scene
        if (Input.GetKeyDown(KeyCode.Escape) || (UnityEngine.XR.XRSettings.isDeviceActive && Input.GetKeyDown("XRI_Right_SecondaryButton")))
        {
            if (SceneManager.GetActiveScene().name == "Switch_v2")
            {
                Application.Quit();
            }
            else
            {
                SceneManager.LoadScene("Switch_v2");
            }
        }
    }
}
