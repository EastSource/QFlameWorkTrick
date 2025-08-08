using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class TrapTrigger : MonoBehaviour, IController
{
    [SerializeField]private abstractTrap trap;
    [SerializeField] private int numberOfTime = -1;
    private int remainingTimes;

    private void Start()
    {
        remainingTimes = numberOfTime;
        this.RegisterEvent<OnReLoaded>(e =>
        {
            remainingTimes = numberOfTime;
        });
    }
    
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
