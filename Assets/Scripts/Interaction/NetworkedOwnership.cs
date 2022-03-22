﻿using System.Collections;
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
    public bool ownership;
    private NetworkContext ctx;

    public delegate void Relinquish();
    private List<Relinquish> release;

    // timestamp to solve synchronization issues
    private DateTime lastOwnedTime;
    class Message
    {
        public DateTime ownTime;
        public Message(DateTime t)
        {
            ownTime = t;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        release = new List<Relinquish>();
        ctx = NetworkScene.Register(this);
    }

    // invoked by external events, e.g. when grasped
    // ATTENTION: never invoke Own() in update()!!
    public void Own(Relinquish r)
    {
        release.Add(r);
        Own();
    }
    public void Own()
    {
        ownership = true;
        lastOwnedTime = DateTime.UtcNow;
        ctx.SendJson<Message>(new Message(lastOwnedTime));
    }
    public void UnOwn(DateTime timeToUnOwn)
    {
        if (timeToUnOwn > lastOwnedTime)
        {
            ownership = false;
            foreach (var r in release)
            {
                r();
            }
            release.Clear();
        }    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<Message>();
        DateTime remoteOwnedTime = msg.ownTime;
        UnOwn(remoteOwnedTime); 
    }
}
