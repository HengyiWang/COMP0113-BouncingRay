using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;
using Ubiq.Samples;
using System;

// ownership mechanism (seizable mutex)
// It is also an INetworkComponent that takes charge of the ownership negotiation
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
        // seal the timestamp
        lastOwnedTime = DateTime.UtcNow;
        // send message to other peers to request UnOwn()
        ctx.SendJson<Message>(new Message(lastOwnedTime));
    }
    public void UnOwn(DateTime timeToUnOwn)
    {
        // check the timestamp, only requests that are later can apply
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
