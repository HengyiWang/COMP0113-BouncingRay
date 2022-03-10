using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using System;

public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    public NetworkId Id { get; set; }
    public void Awake()
    {
        string uniqueTag = transform.position.ToString() +
                            transform.rotation.ToString();
        Id = new NetworkId((uint)uniqueTag.GetHashCode());
    }
}
