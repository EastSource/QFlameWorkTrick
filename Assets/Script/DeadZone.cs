using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QFramework;

public class DeadZone : MonoBehaviour, IController
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.SendCommand<PlayerDeadCommand>();
            Debug.Log("Dead");
        }
    }

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }
}
