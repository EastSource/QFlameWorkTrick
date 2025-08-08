using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class CNeedleGun : abstractTrap, IController
{
    
    [SerializeField] private Vector3 stopPosition;
    //移動先に移動するまでにかかる時間
    [SerializeField] private float moveSpeed = 5f;
    //連続する二つ目のNeedle
    [SerializeField] private CNeedleGun secondNeedle;

    private void Awake()
    {
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
    }
    
    private void Start()
    {
        //イベント登録
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move()).UnRegisterWhenCurrentSceneUnloaded();
        this.RegisterEvent<OnReLoaded>(e => Restart()).UnRegisterWhenCurrentSceneUnloaded();
    }

    public override void Move()
    {
        this.transform.DOMove(desticationPosition, moveSpeed).SetEase(Ease.OutQuad);
        //連続する二つ目のNeedleがあればそれを時間差で動かす
        if (secondNeedle != null)
        {
            StartCoroutine(OnTimer());
        }
    }

    public override void Restart()
    {
        this.transform.position = fixedPosition;
    }

    //衝突したときプレイヤーの死亡処理
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerDeadCommand>();
        }
    }
    
    //非同期処理
    private IEnumerator OnTimer()
    {
        yield return new WaitForSeconds(moveSpeed - 2f);
        secondNeedle.Move();
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;   
    }
}
