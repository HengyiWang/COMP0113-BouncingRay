using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    public NetworkId Id { get; } = NetworkId.Unique();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
