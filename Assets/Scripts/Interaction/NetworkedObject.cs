using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    public NetworkId Id { get; set; }
    public void Awake()
    {
        Id = new NetworkId(transform.position.ToString() +
                            transform.rotation.ToString());
    }
}
