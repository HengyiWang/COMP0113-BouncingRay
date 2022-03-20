using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public class PathHintEvent : LevelEvent
{
    public GameObject objToAdd;
    public List<Transform> destinations;
    public GameObject pathHint;
    public GameObject sphere;
    public Vector3 hintScale;
    public Vector3 hintRotation;

    private WalkHint walkHint;

    protected override bool IsCompleted()
    {
        return walkHint.destinations.Count < destinations.Count || destinations.Count == 0;
    }

    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
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

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    public override void OnEventDestory()
    {
        walkHint.destoryAllPathHints();
        Destroy(walkHint);
    }
}
