using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

// specific class for gun states networking
public class NetworkedGunStates : MonoBehaviour, INetworkComponent
{
    private NetworkContext ctx;
    private NetworkedOwnership ownershipComp;
    private MyFollowGraspable followComp;
    private Shoot shootComp;
    public class Message
    {
        public bool grasped;
        public bool clicked;
        public Message(bool g, bool c)
        {
            grasped = g;
            clicked = c;
        }
    }
    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        if (ownershipComp && !ownershipComp.ownership)
        {
            var msg = message.FromJson<Message>();
            followComp.grasped = msg.grasped;
            shootComp.clicked = msg.clicked;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ctx = NetworkScene.Register(this);
        ownershipComp = GetComponent<NetworkedOwnership>();
        followComp = GetComponent<MyFollowGraspable>();
        shootComp = GetComponent<Shoot>();
        if (!ownershipComp || !followComp || !shootComp)
        {
            Debug.LogError("Necessary component is missing!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (ownershipComp && ownershipComp.ownership)
        {
            Message msg = new Message(followComp.grasped, shootComp.clicked);
            ctx.SendJson<Message>(msg);
        }

    }
}
