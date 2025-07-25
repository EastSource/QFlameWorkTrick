using System.Collections;
using System.Collections.Generic;
using QFramework;
using UnityEngine;

public class PlayerModel : AbstractModel
{
    private Vector3 startPosition;
    private float movementSpeed;
    private float jumpPower;
    bool haveKey;
    
    //アクセサ
    public Vector3 StartPosition { get => startPosition; }
    public float MovementSpeed => movementSpeed;
    public float JumpPower { get => jumpPower; }
    public bool HaveKey => haveKey;
    
    protected override void OnInit()
    {
        startPosition = new Vector3(-20f, -8f, 0f);
        movementSpeed = 7f;
        jumpPower = 30f;
        haveKey = false;
    }
}
