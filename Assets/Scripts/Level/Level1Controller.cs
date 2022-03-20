using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1Controller : LevelController
{
    public List<LevelEvent> endGameEvents;

    protected override void SetUp()
    {
        initialize(endGameEvents);
    }
}
