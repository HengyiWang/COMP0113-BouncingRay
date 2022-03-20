using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level0Controller : LevelController
{
    public List<LevelEvent> guideEvents;

    public List<LevelEvent> tempEvents;

    protected override void SetUp()
    {
        initialize(guideEvents);
    }
}
