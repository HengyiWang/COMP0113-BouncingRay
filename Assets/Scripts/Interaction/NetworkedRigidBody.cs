using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

// applied to objects that have rigidbody component
// and network its useGravity and isKinematic properties
public class NetworkedRigidBody : MonoBehaviour, INetworkComponent
{
    private Rigidbody rb;
    private NetworkedOwnership ownershipComp;
    private NetworkContext ctx;
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        if (ownershipComp && !ownershipComp.ownership)
        {
            var msg = message.FromJson<Message>();
            rb.useGravity = msg.useGravity;
            rb.isKinematic = msg.isKinematic;
        }   
    }

    class Message
    {
        public bool useGravity;
        public bool isKinematic;
        public Message(bool ug, bool ik)
        {
            useGravity = ug;
            isKinematic = ik;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        ctx = NetworkScene.Register(this);
        rb = GetComponent<Rigidbody>();
        ownershipComp = GetComponent<NetworkedOwnership>();
        if (!ownershipComp)
        {
            Debug.LogError("Cannot apply this script to an object without NetworkedOwnership!");
        }
        if (!ownershipComp)
        {
            Debug.LogError("Cannot apply this script to an object without RigidBody!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ownershipComp && ownershipComp.ownership)
        {
            Message msg = new Message(rb.useGravity, rb.isKinematic);
            ctx.SendJson<Message>(msg);
        }
    }
}
