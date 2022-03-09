using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;

public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    // public NetworkId Id { get; } = NetworkId.Unique();
    public NetworkId Id { get; } = new NetworkId(100);
}
