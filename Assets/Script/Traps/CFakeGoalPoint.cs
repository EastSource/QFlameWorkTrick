using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class CFakeGoalPoint : abstractTrap, IController
{
    //移動先に移動するまでにかかる時間
    [SerializeField] private float MovingdurationTime = 2.0f;
    private void Start()
    {
        fixedPosition = this.transform.position;
        desticationPosition = transform.position + new Vector3(45f, 0, 0);
        this.RegisterEvent<OnReLoaded>(e => Restart()).UnRegisterWhenCurrentSceneUnloaded();
    }
    
    public override void Move()
    {
        this.SendCommand<TurnOnFakeGoalPointCommand>();
        this.transform.DOMove(desticationPosition, MovingdurationTime).SetEase(Ease.OutSine);
    }

    public override void Restart()
    {
        Show();
        this.transform.position = fixedPosition;   
    }
    
    //プレイヤーと衝突したとき死亡処理
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerDeadCommand>();
            Hide();
        }
    }
    
    //View
    private void Show()
    {
        this.gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        this.gameObject.SetActive(false);
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }
}
