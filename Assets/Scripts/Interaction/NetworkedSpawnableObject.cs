using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Samples;
using UnityEngine;

public class NetworkedSpawnableObject : NetworkedOwnership, ISpawnable
{
    public NetworkId Id { get; set; } = NetworkId.Unique();

    public void OnSpawned(bool local)
    {
        this.ownership = local;
    }
}
