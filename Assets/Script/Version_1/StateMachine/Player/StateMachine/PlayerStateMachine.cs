using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 对玩家状态进行管理
/// </summary>
public class PlayerStateMachine:StateMachine,IAgent
{


    [SerializeField]public PlayerState[] playerMovementStates;
    public PlayerControl playerController;
    public Animator animator;

    public bool IsHit;

    // 定义一个委托类型，用于传递攻击信息
    public delegate void PlayerAttackEventHandler(AttackInfo info);

    // 定义一个事件，使用委托类型
    public static event PlayerAttackEventHandler OnAttackReceived;

    public void GetDamage(AttackInfo info)
    {
        playerController.CurrentHp-=info.BaseAttack;
        if(playerController.CurrentHp<=0)
        {
            ChangeState(typeof(PlayerDieState));
            return;
        }
        OnAttackReceived.Invoke(info);
    }

    void Awake()
    {
        playerController=GetComponent<PlayerControl>();
        animator=GetComponentInChildren<Animator>();
        stateTable=new Dictionary<System.Type, IState>(playerMovementStates.Length);
        //遍历状态并进行初始化
        foreach(var playerState in playerMovementStates)
        {
            playerState.Initialize(animator,this,playerController);
            stateTable.Add(playerState.GetType(),playerState);
        }
    }




}}