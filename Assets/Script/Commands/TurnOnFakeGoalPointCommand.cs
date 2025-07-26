using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TurnOnFakeGoalPointCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        Debug.Log("FakeTrap");
        this.SendEvent<OnTurnOnFakeGoalPoint>();
    }
}
