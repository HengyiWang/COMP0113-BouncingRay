using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class NetworkedTransform : MonoBehaviour, INetworkComponent, INetworkObject
{
    public NetworkId Id { get; } = new NetworkId();
    NetworkContext ctx;

    struct SynchronizedTransform
    {
        public Vector3 position;
        public Quaternion rotation;
        // public Vector3 scale;
    }

    // private ownership;
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        var msg = message.FromJson<SynchronizedTransform>();
        transform.position = msg.position;
        transform.rotation = msg.rotation;
    }

    // Start is called before the first frame update
    void Start()
    {
        ctx = NetworkScene.Register(this); 
    }

    // Update is called once per frame
    void Update()
    {
        var grasped = GetComponent<MyFollowGraspable>().grasped;
        if (grasped)
        {
            SynchronizedTransform message;
            message.position = transform.position;
            message.rotation = transform.rotation;
            ctx.SendJson(message);
        }
    }
}
