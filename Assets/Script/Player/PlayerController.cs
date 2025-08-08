using System;
using System.Collections;
using System.Collections.Generic;
using QFramework;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour, IController
{
    public static PlayerController Instance {set; get;}

    private bool canMove;
    private bool canJump;
    [SerializeField]private bool isJump;
    private Rigidbody2D rb;
    private PlayerInput playerInput;
    private PlayerModel mPlayer;
    
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
        
        //イベント登録
        this.RegisterEvent<OnReLoaded>(e =>
        {
            Spawn();
        });
        playerInput.Player.Jump.performed += ctx => Jump();
        
        //初期位置にスポーンする
        Spawn();
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

    private void Spawn()
    {
        mPlayer.HaveKey = false;
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

    public IArchitecture GetArchitecture()
    {
        return TrapGameApp.Interface;
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
}
