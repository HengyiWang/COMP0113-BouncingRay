using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using Ubiq.Samples;
using UnityEngine;

// Unique ID generator and ownership negotiation
// for networked objects that are spawned during the game
public class NetworkedSpawnableObject : NetworkedOwnership, ISpawnable
{
    public NetworkId Id { get; set; } = NetworkId.Unique();

    public void OnSpawned(bool local)
    {
        this.ownership = local;
    }
}
