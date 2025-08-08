using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour, IController
{
    [SerializeField]private Text textGoal;
    [SerializeField]private Text textKey;

    private void Start()
    {
        this.RegisterEvent<OnPlayerGetGoalKey>(e => HideKey());
        this.RegisterEvent<OnTurnOnFakeGoalPoint>(e => HideGoal());;
        this.RegisterEvent<OnReLoaded>(e =>
        {
            ShowGoal();
            ShowKey();
        });
    }
    
    //view
    public void HideKey()
    {
        textKey.gameObject.SetActive(false);
    }
    
    public void ShowKey()
    {
        textKey.gameObject.SetActive(true);
    }

    public void HideGoal()
    {
        textGoal.gameObject.SetActive(false);
    }
    
    public void ShowGoal()
    {
        textGoal.gameObject.SetActive(true);   
    }
    
    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface; 
    }
}
