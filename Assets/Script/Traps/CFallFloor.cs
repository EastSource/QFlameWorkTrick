using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CFallFloor : abstractTrap, IController
{
    private Rigidbody rb;
    
    //このオブジェクトには必要がない

    private void Awake()
    {
        fixedPosition = this.transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        this.RegisterEvent<OnPlayerDead>(e =>
        {
            Restart();
        });
    }
    
    public override void Move()
    {
        rb.isKinematic = false;
    }

    public override void Restart()
    {
        rb.isKinematic = true;
        this.transform.position = fixedPosition;
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Move();
        }
    }
}
