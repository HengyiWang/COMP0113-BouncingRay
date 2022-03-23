using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMirror : MonoBehaviour
{
    public bool hitted;
    public bool played;
    public int colorID;
    public Vector3 hitPoint = new Vector3(0,0,0);

    public void playAudioSource()
    {
        if (!played)
        {
            GetComponent<AudioSource>().Play();
            played = true;
        }
    }
}
