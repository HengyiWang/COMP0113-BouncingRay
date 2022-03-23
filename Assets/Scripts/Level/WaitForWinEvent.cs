using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// complete only robot energy is full
public class WaitForWinEvent : LevelEvent
{
    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {

    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    protected override bool IsCompleted()
    {
        return GameObject.Find("Robot").GetComponent<Robots>().energy;
    }
}