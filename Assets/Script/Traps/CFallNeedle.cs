using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CFallNeedle : abstractTrap, IController
{
    Rigidbody2D rb;
    private void Awake()
    {
        fixedPosition = this.transform.position;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Restart();
        });
        rb.isKinematic = true;
    }
    public override void Move()
    {
        rb.isKinematic = false;
        rb.AddForce(Vector2.down * 70, ForceMode2D.Impulse);
    }

    public override void Restart()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        this.transform.position = fixedPosition;   
    }

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
