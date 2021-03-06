using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robots : MonoBehaviour
{
    Animator m_Animator;
    public bool isHitted;
    public bool energy;
    public bool played;
    public AudioSource[] sound;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        m_Animator.SetBool("IsHitted", isHitted);
        
        if (energy)
        {
            
            m_Animator.SetBool("Energy", energy);
            playAudioSource();

        }
    }

    void playAudioSource()
    {
        if (!played)
        {
            sound = GetComponents<AudioSource>();
            sound[0].Play();
            played = true;
        }
    }
}
