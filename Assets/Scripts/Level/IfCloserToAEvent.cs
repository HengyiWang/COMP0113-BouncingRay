using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;

public class IfCloserToAEvent : IfEvent
{
    public GameObject player;
    public GameObject A;
    public GameObject B;

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
        Vector3 playerPosition = player.transform.position;
        return Vector3.Distance(playerPosition, A.transform.position) < Vector3.Distance(playerPosition, B.transform.position);
    }
}
