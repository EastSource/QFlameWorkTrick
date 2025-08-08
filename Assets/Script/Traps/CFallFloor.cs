using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CFallFloor : abstractTrap, IController
{
    private Rigidbody2D rb;
    
    //このオブジェクトには必要がない

    private void Awake()
    {
        fixedPosition = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        rb.isKinematic = true;
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Restart();
        });
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
        Debug.Log(rb.isKinematic);
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Move();
        }
    }
}
