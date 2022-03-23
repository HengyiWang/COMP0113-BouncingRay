using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using System;
using Ubiq.Rooms;

// Unique ID generator and ownership negotiation
// for networked objects that are present in the beginning of the game
public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    public NetworkId Id { get; set; }

    static public RoomClient GetRoomClient()
    {
        var networkScene = GameObject.Find("Network Scene");
        if (!networkScene)
        {
            Debug.LogError("No NetworkScene!");
        }
        return networkScene.GetComponent<RoomClient>();
    }

    protected override void Start()
    {
        base.Start();
        var rc = GetRoomClient();
        rc.OnJoinedRoom.AddListener(OwnOnJoinedRoom);
        Own();
    }

    public void OwnOnJoinedRoom(IRoom room)
    {
        Own();
    }

    void Awake()
    {
        // generate unique ID based on positions and rotations
        string uniqueTag = transform.position.ToString() +
                            transform.rotation.ToString();
        Id = new NetworkId((uint)uniqueTag.GetHashCode());
    }
}
