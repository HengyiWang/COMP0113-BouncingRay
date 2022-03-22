using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class WaitForWinEvent : LevelEvent
{
    private ScoreManager scoreManager;

    protected override void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        scoreManager = GameObject.Find("ScoreBox").GetComponent<ScoreManager>();
    }

    protected override void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents)
    {
        
    }

    protected override bool IsCompleted()
    {
        return scoreManager.score == scoreManager.total && GameObject.Find("Robot").GetComponent<Robots>().isHitted;
    }
}
