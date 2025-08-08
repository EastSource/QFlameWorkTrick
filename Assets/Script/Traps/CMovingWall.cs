using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class CMovingWall : abstractTrap, IController
{
    [SerializeField] private Vector3 stopPosition;
    //移動先に移動するまでにかかる時間
    [SerializeField] private float moveSpeed = 5f;
    private void Start()
    {
        //変数初期化
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
        
        //イベント登録
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move()).UnRegisterWhenCurrentSceneUnloaded();
        this.RegisterEvent<OnReLoaded>(e => Restart()).UnRegisterWhenCurrentSceneUnloaded();
    }

    public override void Move()
    {
        this.transform.DOMove(desticationPosition, moveSpeed).SetEase(Ease.OutQuad).SetLoops(2, LoopType.Yoyo);
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
