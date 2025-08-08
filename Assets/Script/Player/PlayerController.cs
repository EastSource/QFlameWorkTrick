using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    //シングルトンパターン
    public static PlayerController Instance {set; get;}

    private bool canMove;
    private bool canJump;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerModel mPlayer;

    [SerializeField]private bool isJump;
    //ステージにゴールキーがある場合True、無い場合False
    [SerializeField]private bool initHasKey = false;
    
    private void Awake()
    {
        Instance = this;
        canMove = true;
        canJump = true;
    }
    
    private void Start()
    {
        //各コンポーネントの取得
        playerInput = new PlayerInput();
        mPlayer = this.GetModel<PlayerModel>();
        rb = GetComponent<Rigidbody2D>();
        playerInput.Player.Enable();
        mPlayer.HaveKey = initHasKey;
        
        //イベント登録
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Spawn();
        }).UnRegisterWhenCurrentSceneUnloaded();
        playerInput.Player.Jump.performed += ctx => Jump();
        //初期位置にスポーンする
        Spawn();
    }
    
    //イベントをUnRegister
    private void OnDestroy()
    {
        playerInput.Player.Jump.performed -= ctx => Jump();
        playerInput.Player.Disable();
    }
    
    private void Update()
    {
        if (canMove)
        {
            Movement();
        }
    }
    
    private void Movement()
    {
        float inputVector = playerInput.Player.Moving.ReadValue<float>();
        transform.position += new Vector3(inputVector * mPlayer.MovementSpeed, 0, 0) * Time.deltaTime;
    }

    //スポーン、リスポーン両方
    private void Spawn()
    {
        mPlayer.HaveKey = initHasKey;
        rb.isKinematic = true;
        transform.position = mPlayer.StartPosition;
        rb.isKinematic = false;
    }

    private void Jump()
    {
        if (!isJump && canJump)
        {
            rb.AddForce(Vector3.up * mPlayer.JumpPower, ForceMode2D.Impulse);
        }
    }

    //PlayerDeadCommandクラスで使われている
    //一時的にプレイヤーが操作‘を行えないようにするためのコマンド
    public void RestartPlayerControle()
    {
        canMove = true;
        canJump = true;
    }

    public void PosePlayerControle()
    {
        canMove = false;
        canJump = false;
    }

    
    //floorに接していればisJump = false,　接していなければtrue
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJump = false;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJump = true;
        }
    }
    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
    }
}
