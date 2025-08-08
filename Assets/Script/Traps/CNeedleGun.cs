using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class CNeedleGun : abstractTrap, IController
{
    
    [SerializeField] private Vector3 stopPosition;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private CNeedleGun secondNeedle;
    private void Start()
    {
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move());
        this.RegisterEvent<OnReLoaded>(e => Restart());
    }

    public override void Move()
    {
        this.transform.DOMove(desticationPosition, moveSpeed).SetEase(Ease.OutQuad);
        if (secondNeedle != null)
        {
            StartCoroutine(OnTimer());
        }
    }

    public override void Restart()
    {
        this.transform.position = fixedPosition;
    }

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
