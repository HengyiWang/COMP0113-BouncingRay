using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ubiq.Messaging;

public class NetworkedFlashlightStates : MonoBehaviour, INetworkComponent
{
    private NetworkContext ctx;
    private NetworkedOwnership ownershipComp;
    private MyFollowGraspable followComp;
    private ShootLighting shootComp;
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
        var msg = message.FromJson<Message>();
        followComp.grasped = msg.grasped;
        shootComp.clicked = msg.clicked;
    }
    // Start is called before the first frame update
    void Start()
    {
        ctx = NetworkScene.Register(this);
        ownershipComp = GetComponent<NetworkedOwnership>();
        followComp = GetComponent<MyFollowGraspable>();
        shootComp = GetComponent<ShootLighting>();
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
