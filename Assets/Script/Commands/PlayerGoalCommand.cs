using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class PlayerGoalCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        if (this.GetModel<PlayerModel>().HaveKey)
        {
            this.SendEvent<OnReLoaded>();
            Debug.Log("Goal");
        }
    }
}
