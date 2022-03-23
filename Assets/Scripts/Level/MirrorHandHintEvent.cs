using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

// hint mirror hint event
public class MirrorHandHintEvent : LevelEvent
{
    public GameObject hand;
    public Transform handTransform;

    private GameObject instance;  // the instance we created

    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        instance = Instantiate(hand, handTransform);
    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    protected override bool IsCompleted()
    {
        // if any gem is grapsed, then this event is completed
        foreach (Transform child in GameObject.Find("All_Gem").transform)
        {
            if (child.GetComponent<MirrorGraspable>().grasped)
            {
                return true;
            }
        }
        return false;
    }

    public override void OnEventDestory()
    {
        Destroy(instance);
    }
}
