using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public class CPopUpNeedle : abstractTrap, IController
{
    private void Awake()
    {
        fixedPosition = this.transform.position;//初期位置
        desticationPosition = transform.position + new Vector3(0, 1, 0);//移動先
    }

    private void Start()
    {
        //リロードイベント登録
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Restart();
        }).UnRegisterWhenCurrentSceneUnloaded();
    }
    
    public override void Move()
    {
        this.transform.DOMove(desticationPosition, 0.5f).SetEase(Ease.OutExpo);
    }

    public override void Restart()
    {
        this.transform.position = fixedPosition;
    }

    //プレイヤーと衝突したとき死亡処理
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerDeadCommand>();
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }
}
