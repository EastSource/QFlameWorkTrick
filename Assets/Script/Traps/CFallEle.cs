using System.Collections;
using System.Collections.Generic;
using QFramework;
using DG.Tweening;
using UnityEngine;

public class CFallEle : abstractTrap, IController
{
    [SerializeField] private Vector3 stopPosition;
    
    //移動先に移動するまでにかかる時間
    [SerializeField] private float moveSpeed = 5f;
    Rigidbody2D rb;

    private void Awake()
    {
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
    }
    
    private void Start()
    {
        //コンポーネント取得
        rb = GetComponent<Rigidbody2D>();
        
        //落下を禁止するための処理
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        
        //イベント登録
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move()).UnRegisterWhenCurrentSceneUnloaded();
        this.RegisterEvent<OnReLoaded>(e => Restart()).UnRegisterWhenCurrentSceneUnloaded();
    }
    
    public override void Move()
    {
        this.transform.DOMove(desticationPosition, moveSpeed).SetEase(Ease.InSine);
        StartCoroutine(OnTimer());
    }

    public override void Restart()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        this.transform.position = fixedPosition;
    }
    
    //非同期処理
    private IEnumerator OnTimer()
    {
        yield return new WaitForSeconds(5f);
        DOTween.Kill(this);
        rb.isKinematic = false;
        rb.velocity = Vector2.down * 30;
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;  
    }
}
