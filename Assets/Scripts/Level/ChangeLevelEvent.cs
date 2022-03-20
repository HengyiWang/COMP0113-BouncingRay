using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class ChangeLevelEvent : LevelEvent
{

    public GameObject teleportor;
    public Vector3 position;

    private GameObject teleportorInstance;

    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        teleportorInstance = Instantiate(teleportor, position, Quaternion.identity);
    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    protected override bool IsCompleted()
    {
        return false;
    }

    public override void OnEventDestory()
    {
        Destroy(teleportorInstance);
    }
}
