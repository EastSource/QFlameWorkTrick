using System.Collections;
using DG.Tweening;
using QFramework;
using UnityEngine;

public class PlayerDeadCommand : AbstractCommand
{
    protected override void OnExecute()
    {
        PlayerController.Instance.StartCoroutine(OnDead());
    }

    //After wait 1sec, InitStage and Respawn Player
    private IEnumerator OnDead()
    {
        PlayerController.Instance.PosePlayerControle();
        yield return new WaitForSeconds(1f);
        DOTween.KillAll();
        PlayerController.Instance.RestartPlayerControle();
        this.SendEvent<OnReLoaded>();
    }
}
