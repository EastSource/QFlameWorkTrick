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
    private bool isJump;
    private Rigidbody rb;
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
        playerInput = new PlayerInput();
        mPlayer = this.GetModel<PlayerModel>();
        rb = GetComponent<Rigidbody>();
        playerInput.Player.Enable();
        
        //イベント登録
        this.RegisterEvent<OnPlayerDead>(e =>
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
        transform.position = mPlayer.StartPosition;
    }

    private void Jump()
    {
        if (isJump && !canJump) return;
        rb.AddForce(Vector3.up * mPlayer.JumpPower, ForceMode.Impulse);
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
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJump = false;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("floor"))
        {
            isJump = true;
        }
    }
}
