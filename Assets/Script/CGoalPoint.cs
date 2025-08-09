using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CGoalPoint : MonoBehaviour, IController
{
    //ゴールした時の遷移先
    [SerializeField] private SceneAsset sceneToLoad;
    private void Start()
    {
        Load();
        
        //イベントの登録
        this.RegisterEvent<OnTurnOnFakeGoalPoint>(e => Show()).UnRegisterWhenCurrentSceneUnloaded();
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Load();
        }).UnRegisterWhenCurrentSceneUnloaded();
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
    
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
    //ここから
    private void Load()
    {
        if (this.GetModel<PlayerModel>().HaveKey)
        {
            Show();
        }else
        {
            Hide();
        }
    }
    //ここまで追加
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
   //------------------------------------------------------------------------------------------------------------- 
    
   //ゴールした時の処理`
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerGoalCommand>();
            SceneManager.LoadScene(sceneToLoad.name);
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;  
    }
}
