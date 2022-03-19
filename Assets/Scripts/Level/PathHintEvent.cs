using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathHintEvent : LevelEvent
{
    private GameObject objToAdd;
    private List<Transform> destinations;
    private GameObject pathHint;
    private GameObject sphere;
    private Vector3 hintScale;
    private Vector3 hintRotation;

    private WalkHint walkHint;

    public PathHintEvent(GameObject objToAdd, List<Transform> destinations, GameObject sphere, GameObject pathHint, Vector3 hintScale, Vector3 hintRotation)
    {
        this.objToAdd = objToAdd;
        this.destinations = destinations;
        this.pathHint = pathHint;
        this.hintScale = hintScale;
        this.hintRotation = hintRotation;
        this.sphere = sphere;
    }

    protected override bool IsCompleted()
    {
        return walkHint.destinations.Count < destinations.Count;  // reach any destination is complete
    }

    protected override void EventStart()
    {
        walkHint = objToAdd.AddComponent<WalkHint>();
        walkHint.sphere = sphere;
        walkHint.pathHint = pathHint;
        walkHint.hintScale = hintScale;
        walkHint.hintRotation = hintRotation;
        walkHint.destinations = new List<Transform>();
        foreach (Transform transform in destinations)
        {
            walkHint.destinations.Add(transform);
        }
    }

    protected override void EventUpdate()
    {
        
    }

    public override void OnDestory()
    {
        GameObject.Destroy(walkHint);
    }
}
