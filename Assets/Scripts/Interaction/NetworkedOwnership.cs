using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Samples;
using System;

// set the basics for network object that is already present at the start of the game
// 1. network id
// 2. ownership
public class NetworkedOwnership : MonoBehaviour, INetworkComponent
{
    // if true, this object is owned locally, otherwise remotely
    public bool ownership = false;
    public delegate void Relinquish();
    private NetworkContext ctx;
    private List<Relinquish> release;


    // Start is called before the first frame update
    void Start()
    {
        ctx = NetworkScene.Register(this);
    }

    // invoked by external events, e.g. when grasped
    // ATTENTION: never invoke Own() in update()!!
 
    public void Own(Relinquish r)
    {
        ownership = true;
        release.Add(r);
        ctx.SendJson<bool>(true);
    }

    public void UnOwn()
    {
        ownership = false;
        foreach(var r in release)
        {
            r();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        UnOwn();
    }
}
