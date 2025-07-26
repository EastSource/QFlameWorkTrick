using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CGoalPoint : MonoBehaviour, IController
{
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private MeshCollider meshCollider;
    private void Start()
    {
        this.RegisterEvent<OnTurnOnFakeGoalPoint>(e => Show());
        this.RegisterEvent<OnPlayerDead>(e => Hide());
        Hide();
    }
    
    //view
    private void Show()
    {
        this.gameObject.SetActive(true); 
    }

    private void Hide()
    {
        this.gameObject.SetActive(false);   
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Goal");
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;  
    }
}
