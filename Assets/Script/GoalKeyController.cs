using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class GoalKeyController : MonoBehaviour, IController
{
    [SerializeField]private Text text;
    private void Start()
    {
        this.RegisterEvent<OnPlayerGetGoalKey>(e => Hide());
        this.RegisterEvent<OnReLoaded>(e => Show());
    }

    void OnTriggerEnter2D(Collider2D other)
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
