using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.XR;
using UnityEngine;

public class grasp : MonoBehaviour, IGraspable, INetworkComponent, INetworkObject
{
    private Hand grasped;

    public NetworkId Id => new NetworkId(10043);

    // Store the association between network object and
    // the corresponding scene
    private NetworkContext context;

    public void Grasp(Hand controller)
    {
        // Call when grasp the object
        grasped = controller;
    }

    public void ProcessMessage(ReferenceCountedSceneGraphMessage message)
    {
        print("I have received the msg");
        var msg = message.FromJson<Message>();
        transform.localPosition = msg.position;
    }

    public void Release(Hand controller)
    {
        // Call when release the object
        grasped = null;
    }

    // Start is called before the first frame update
    void Start()
    {
        context = NetworkScene.Register(this);

    }

    struct Message
    {
        public Vector3 position;
    }

    // Update is called once per frame
    void Update()
    {
        if (grasped)
        {
            transform.localPosition = grasped.transform.position;
            Message message;
            message.position = transform.localPosition;
            context.SendJson(message);
        }

    }
}
