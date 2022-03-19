using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class LevelEvent
{

    public UnityEvent OnComplete;

    // Start is called before the first frame update
    public void Start()
    {
        OnComplete = new UnityEvent();
        EventStart();
    }

    // Update is called once per frame
    public void Update()
    {
        EventUpdate();

        if (IsCompleted())
        {
            OnComplete.Invoke();
        }
    }

    public abstract void OnDestory();

    protected abstract void EventStart();

    protected abstract void EventUpdate();

    protected abstract bool IsCompleted();

}
