using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2Controller : LevelController
{
    public List<LevelEvent> endGameEvents;

    protected override void SetUp()
    {
        initialize(endGameEvents);
    }
}
