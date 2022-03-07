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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
