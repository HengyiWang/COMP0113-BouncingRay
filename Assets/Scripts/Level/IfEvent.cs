using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// branching event
public abstract class IfEvent : LevelEvent
{
    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        List<LevelEvent> eventsToAdd = condition() ? caseTrueEvents() : caseFalseEvents();

        foreach (LevelEvent eventToAdd in eventsToAdd) {
            followingEvents.Enqueue(eventToAdd);
        }
    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    protected override bool IsCompleted()
    {
        return true;
    }

    protected abstract bool condition();

    protected abstract List<LevelEvent> caseTrueEvents();

    protected abstract List<LevelEvent> caseFalseEvents();
}
