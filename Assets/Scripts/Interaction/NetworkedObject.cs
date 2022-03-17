using System.Collections;
using System.Collections.Generic;
using Ubiq.Messaging;
using UnityEngine;
using System;
using Ubiq.Rooms;

public class NetworkedObject : NetworkedOwnership, INetworkObject
{
    public NetworkId Id { get; set; }

    static public RoomClient GetRoomClient()
    {
        var networkScene = GameObject.Find("NetworkScene");
        if (!networkScene)
        {
            Debug.LogError("No NetworkScene!");
        }
        return networkScene.GetComponent<RoomClient>();
    }

    public void OwnOnJoinedRoom(IRoom room)
    {
        Own();
    }
    public void Awake()
    {
        var rc = GetRoomClient();
        rc.OnJoinedRoom.AddListener(OwnOnJoinedRoom);

        string uniqueTag = transform.position.ToString() +
                            transform.rotation.ToString();
        Id = new NetworkId((uint)uniqueTag.GetHashCode());
    }
}
