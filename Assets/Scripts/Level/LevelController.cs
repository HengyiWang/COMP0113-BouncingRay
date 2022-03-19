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
        private Queue<LevelEvent> chain;
        public LevelEvent runningEvent = null;

        public EventChain(LevelEvent levelEvent)
        {
            chain = new Queue<LevelEvent>();
            chain.Enqueue(levelEvent);
        }

        public EventChain Then(LevelEvent levelEvent)
        {
            chain.Enqueue(levelEvent);
            return this;
        }

        public void Run()
        {
            if (runningEvent != null)
            {
                runningEvent.OnDestory();
                runningEvent = null;
            }
            if (chain.Count > 0)
            {
                runningEvent = chain.Dequeue();
                runningEvent.Start();
                runningEvent.OnComplete.AddListener(this.Run);
            }
        }

        public bool IsFinished()
        {
            return chain.Count == 0 && runningEvent == null;
        }
    }

    void Start()
    {
        initEventChains = new List<EventChain>();
        finishEventChains = new List<EventChain>();

        ControllerStart();

        foreach (EventChain chain in initEventChains)
        {
            chain.Run();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControllerUpdate();

        EventChainUpdate(initEventChains);
        EventChainUpdate(finishEventChains);

        if (IsTimeForFinishEvent())
        {
            foreach (EventChain chain in finishEventChains)
            {
                chain.Run();
            }
        }
    }

    private void EventChainUpdate(List<EventChain> eventChainList)
    {
        foreach (EventChain chain in eventChainList)
        {
            if (chain.runningEvent != null && !chain.IsFinished())
            {
                chain.runningEvent.Update();
            }
        }
    }

    private bool IsTimeForFinishEvent()
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

    protected abstract void ControllerStart();

    protected abstract void ControllerUpdate();
}
