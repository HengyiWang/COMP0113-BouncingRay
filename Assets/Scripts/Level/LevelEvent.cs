using System.Collections.ObjectModel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public abstract class LevelEvent : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent OnComplete = new UnityEvent();

    public ReadOnlyCollection<LevelEvent> previousEvents;
    public Queue<LevelEvent> followingEvents;

    public void Awake()
    {
        enabled = false;
    }

    public void Start()
    {
        EventStart(previousEvents, followingEvents);

    }

    public void Update()
    {
        if (IsCompleted())
        {
            OnComplete.Invoke();
        }

        EventUpdate(previousEvents, followingEvents);
    }

    protected abstract void EventStart(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents);

    protected abstract void EventUpdate(ReadOnlyCollection<LevelEvent> previousEvents, Queue<LevelEvent> followingEvents);

    protected abstract bool IsCompleted();

    public virtual void OnEventDestory() { }

}
