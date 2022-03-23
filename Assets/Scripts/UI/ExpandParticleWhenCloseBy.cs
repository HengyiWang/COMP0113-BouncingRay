using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandParticleWhenCloseBy : MonoBehaviour
{

    public float closeByDistance = 30f;
    public GameObject optionalPlayer;

    void Start()
    {
        GetComponent<ParticleSystem>().Stop();
        if (optionalPlayer == null)
        {
            optionalPlayer = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (distanceToPlayer() < closeByDistance)
        {
            if (particleSystem.isStopped)
            {
                particleSystem.Play();
            }
        }
        else if(particleSystem.isPlaying)
        {
            particleSystem.Stop();
        }

    }

    float distanceToPlayer()
    {
        return Vector3.Distance(optionalPlayer.transform.position, transform.position);
    }
}
