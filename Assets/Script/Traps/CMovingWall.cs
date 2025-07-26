using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class CMovingWall : abstractTrap, IController
{
    [SerializeField] private Vector3 stopPosition;
    private void Start()
    {
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move());
        this.RegisterEvent<OnPlayerDead>(e => Restart());
    }

    public override void Move()
    {
        this.transform.DOMove(desticationPosition, 5f).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
    }

    public override void Restart()
    {
        this.transform.position = fixedPosition;
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;   
    }
}
