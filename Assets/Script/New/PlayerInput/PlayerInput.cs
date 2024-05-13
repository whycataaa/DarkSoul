using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 完成按键对行为的绑定
/// </summary>
public class PlayerInput : Singleton<PlayerInput>
{
    public static PlayerInputAsset playerInputAsset{get;private set;}

    public PlayerInputAsset.GamePlayActions gamePlayActions;
    //判断跳跃
    public bool IsJump{get;private set;}=false;
    public bool IsDash{get;private set;}=false;
    public bool IsRoll{get;private set;}=false;
    public bool IsWalk{get;private set;}=false;

    //判断移动
    public float MoveVector2X{get;private set;}
    public float MoveVector2Y{get;private set;}
    public Vector2 MoveVector2{get;private set;}
    //判断鼠标是否显示
    public bool IsShowCursor{get;private set;}=false;

    public bool IsAttack=>playerInputAsset.GamePlay.Attack.WasPressedThisFrame();
    public bool IsDefense=>playerInputAsset.GamePlay.Defense.IsPressed();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if(playerInputAsset==null)
        {
            playerInputAsset=new PlayerInputAsset();
        }
        gamePlayActions=playerInputAsset.GamePlay;
    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        gamePlayActions.Enable();
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        gamePlayActions.Disable();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {

        //激活ActionMap
        playerInputAsset.GamePlay.Enable();

        playerInputAsset.GamePlay.CursorShow.performed+=CursorShow;
        playerInputAsset.GamePlay.CursorShow.canceled+=CursorDisShow;
        playerInputAsset.GamePlay.Jump.started+=StartJump;
        playerInputAsset.GamePlay.Jump.canceled+=StopJump;
        playerInputAsset.GamePlay.Roll.started+=StartRoll;
        playerInputAsset.GamePlay.Roll.canceled+=StopRoll;
        playerInputAsset.GamePlay.Walk.started+=ChangeWalk;


    }

    private void ChangeWalk(InputAction.CallbackContext context)
    {
        IsWalk=!IsWalk;
    }


    private void CursorShow(InputAction.CallbackContext context)
    {
        IsShowCursor=true;
    }

    private void CursorDisShow(InputAction.CallbackContext context)
    {
        IsShowCursor=false;
    }

    private void StartJump(InputAction.CallbackContext context)
    {
        IsJump=true;
    }
    private void StopJump(InputAction.CallbackContext context)
    {
        IsJump=false;
    }
    private void StartRoll(InputAction.CallbackContext context)
    {
        IsRoll=true;
    }
    private void StopRoll(InputAction.CallbackContext context)
    {
        IsRoll=false;
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        //设置MoveVector2的值
        MoveVector2=playerInputAsset.GamePlay.Move.ReadValue<Vector2>();
        MoveVector2X = playerInputAsset.GamePlay.Move.ReadValue<Vector2>().x;
        MoveVector2Y = playerInputAsset.GamePlay.Move.ReadValue<Vector2>().y;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        
    }

}
