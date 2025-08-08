using System.Collections;
using System.Collections.Generic;
using QFramework;
using DG.Tweening;
using UnityEngine;

public class CFallEle : abstractTrap, IController
{
    [SerializeField] private Vector3 stopPosition;
    [SerializeField] private float moveSpeed = 5f;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fixedPosition = this.transform.position;
        desticationPosition = stopPosition;
        rb.isKinematic = true;
        rb.velocity = Vector2.zero;
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Move());
        this.RegisterEvent<OnReLoaded>(e => Restart());
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
