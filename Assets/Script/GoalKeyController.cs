using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class GoalKeyController : MonoBehaviour, IController
{
    private void Start()
    {
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Hide());
        this.RegisterEvent<OnPlayerDead>(e => Show());
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Get Key");
            this.SendCommand<PlayerGetGoalKeyCommand>();
        }
    }
    
    //View
    public void Show()
    {
        gameObject.SetActive(true);  
    }

    public void Hide()
    {
        gameObject.SetActive(false);   
    }
    
    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;   
    }
}
