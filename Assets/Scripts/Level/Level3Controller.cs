using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3Controller : LevelController
{
    public List<LevelEvent> endGameEvents;

    protected override void SetUp()
    {
        initialize(endGameEvents);
    }
}
