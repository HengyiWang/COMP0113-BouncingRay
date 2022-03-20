﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestIfEvent : IfEvent
{

    public List<LevelEvent> trueEvents;
    public List<LevelEvent> falseEvents;

    protected override List<LevelEvent> caseFalseEvents()
    {
        return falseEvents;
    }

    protected override List<LevelEvent> caseTrueEvents()
    {
        return trueEvents;
    }

    protected override bool condition()
    {
        return GameObject.Find("YesCondition") == null;
    }
}
