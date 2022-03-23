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
        // find the room client in the network scene
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
        // try to own when join room
        rc.OnJoinedRoom.AddListener(OwnOnJoinedRoom);
        // try to own on start
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
