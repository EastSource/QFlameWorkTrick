using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class CGoalPoint : MonoBehaviour, IController
{
    [SerializeField] private MeshRenderer meshRenderer;
    private void Start()
    {
        this.RegisterEvent<OnTurnOnFakeGoalPoint>(e => Show());
        this.RegisterEvent<OnReLoaded>(e =>
        {
            if (this.GetModel<PlayerModel>().HaveKey)
            {
                Show();
            }else
            {
                Hide();
            }
        });
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerGoalCommand>();
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;  
    }
}
