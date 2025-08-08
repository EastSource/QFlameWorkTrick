using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CFallFloor : abstractTrap, IController
{
    private Rigidbody2D rb;

    private void Awake()
    {
        //変数
        fixedPosition = this.transform.position;
        
        //コンポーネントを取得
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        //落下を禁止
        rb.isKinematic = true;
        
        //イベント登録
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Restart();
        }).UnRegisterWhenCurrentSceneUnloaded();
    }
    
    public override void Move()
    {
        rb.isKinematic = false;
        Debug.Log(rb.isKinematic);
        
    }

    public override void Restart()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        this.transform.position = fixedPosition;
    }
    
    //衝突したとき落下する
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Move();
        }
    }
    
    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }

    
}
