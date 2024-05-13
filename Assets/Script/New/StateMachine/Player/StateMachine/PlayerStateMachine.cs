using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using UnityEngine;

/// <summary>
/// 对移动状态进行管理
/// </summary>
public class PlayerStateMachine:StateMachine
{


    [SerializeField]public PlayerState[] playerMovementStates;
    public PlayerControl playerController;
    public Animator animator;

    void Awake()
    {
        playerController=GetComponent<PlayerControl>();
        animator=playerController.animator;
        stateTable=new Dictionary<System.Type, IState>(playerMovementStates.Length);
        //遍历状态并进行初始化
        foreach(var playerState in playerMovementStates)
        {
            playerState.Initialize(animator,this,playerController);
            stateTable.Add(playerState.GetType(),playerState);
        }
    }





}