using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : LevelController
{
    public GameObject player;
    public List<Transform> destinations;
    public GameObject sphere;
    public GameObject pathHint;
    public Vector3 hintScale;
    public Vector3 hintRotation;

    public GameObject pointer;

    protected override void ControllerStart()
    {
        OnInit(new PathHintEvent(player, destinations, sphere, pathHint, hintScale, hintRotation))
            .Then(new PointerHintEvent(pointer, new Vector3(1.413f, -30, 0), new Vector3(0.06f, 0.06f, 0.06f), GetComponent<NetworkedOwnership>()));
    }

    protected override void ControllerUpdate()
    {
        
    }
}
