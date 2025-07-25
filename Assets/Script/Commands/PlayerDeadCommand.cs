using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class PlayerDeadCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        DOTween.KillAll();
        this.SendEvent<OnPlayerDead>();
    }
}
