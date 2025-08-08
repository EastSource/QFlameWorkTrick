using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TrapTrigger : MonoBehaviour, IController
{
    [SerializeField]private abstractTrap trap;
    
    //Trapが発生する回数の上限
    [SerializeField] private int numberOfTime = -1;
    
    //Trapが動作する残数
    private int remainingTimes;//Trapが動作する残数

    private void Start()
    {
        remainingTimes = numberOfTime;
        
        //イベント登録
        this.RegisterEvent<OnReLoaded>(e =>
        {
            remainingTimes = numberOfTime;
        }).UnRegisterWhenCurrentSceneUnloaded();
    }
    
    //衝突したとき特定のトラップを動作させる
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && remainingTimes != 0)
        {
            trap.Move();
            remainingTimes--;
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface; 
    }
}
