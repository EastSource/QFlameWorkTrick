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
        fixedPosition = this.transform.position;
        desticationPosition = transform.position + new Vector3(0, 1, 0);
    }

    private void Start()
    {
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Restart();
        });
    }
    
    public override void Move()
    {
        this.transform.DOMove(desticationPosition, 0.5f).SetEase(Ease.OutExpo);
    }

    public override void Restart()
    {
        this.transform.position = fixedPosition;
    }

    private void OnTriggerEnter(Collider other)
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
