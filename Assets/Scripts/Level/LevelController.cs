using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelController : MonoBehaviour
{
    private List<EventChain> initEventChains;
    private List<EventChain> finishEventChains;

    protected sealed class EventChain
    {
        private Queue<LevelEvent> followingEvents;
        private List<LevelEvent> previousEvents;
        public LevelEvent runningEvent = null;

        public EventChain()
        {
            followingEvents = new Queue<LevelEvent>();
            previousEvents = new List<LevelEvent>();
        }

        public EventChain(LevelEvent levelEvent) : this()
        {
            followingEvents.Enqueue(levelEvent);
        }

        public EventChain Then(LevelEvent levelEvent)
        {
            followingEvents.Enqueue(levelEvent);
            return this;
        }

        public void Run()
        {
            if (runningEvent != null)
            {
                runningEvent.OnEventDestory();
                runningEvent.enabled = false;
                previousEvents.Add(runningEvent);
                runningEvent = null;
            }
            if (followingEvents.Count > 0)
            {
                runningEvent = followingEvents.Dequeue();
                runningEvent.previousEvents = previousEvents.AsReadOnly();
                runningEvent.followingEvents = followingEvents;
                runningEvent.OnComplete.AddListener(this.Run);
                runningEvent.enabled = true;
            }
        }

        public bool IsFinished()
        {
            return followingEvents.Count == 0 && runningEvent == null;
        }
    }

    void Start()
    {
        initEventChains = new List<EventChain>();
        finishEventChains = new List<EventChain>();

        SetUp();

        foreach (EventChain chain in initEventChains)
        {
            chain.Run();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTimeToStartFinishEvent())
        {
            foreach (EventChain chain in finishEventChains)
            {
                chain.Run();
            }
        }
    }

    private bool IsTimeToStartFinishEvent()
    {
        foreach (EventChain chain in initEventChains)
        {
            if (!chain.IsFinished())
            {
                return false;
            }
        }

        foreach (EventChain chain in finishEventChains)
        {
            // already running finish event
            if (chain.runningEvent != null)
            {
                return false;
            }
        }

        return true;
    }

    protected EventChain OnInit(LevelEvent levelEvent)
    {
        EventChain chain = new EventChain(levelEvent);
        initEventChains.Add(chain);
        return chain;
    }

    protected EventChain OnFinish(LevelEvent levelEvent)
    {
        EventChain chain = new EventChain(levelEvent);
        finishEventChains.Add(chain);
        return chain;
    }

    protected abstract void SetUp();


    // helper methods
    protected void initialize(List<LevelEvent> events)
    {
        if (events.Count == 0)
        {
            return;
        }
        EventChain eventChain = OnInit(events[0]);
        for (int i = 1; i < events.Count; i++)
        {
            eventChain = eventChain.Then(events[i]);
        }
    }

}
