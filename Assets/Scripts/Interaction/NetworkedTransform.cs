using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class NetworkedTransform : MonoBehaviour, INetworkComponent
{
    private NetworkContext ctx;
    private NetworkedOwnership ownershipComp;
    struct Message
    {
        public Vector3 position;
        public Quaternion rotation;
        // public Vector3 scale;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        if (ownershipComp && !ownershipComp.ownership)
        {
            var msg = message.FromJson<Message>();
            transform.position = msg.position;
            transform.rotation = msg.rotation;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ownershipComp = GetComponent<NetworkedOwnership>();
        ctx = NetworkScene.Register(this); 
    }

    // Update is called once per frame
    void Update()
    {
        if (ownershipComp && ownershipComp.ownership)
        {
            Message message;
            message.position = transform.position;
            message.rotation = transform.rotation;
            ctx.SendJson(message);
        }
    }
}
