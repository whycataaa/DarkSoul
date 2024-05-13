using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 空闲状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Idle",fileName ="Idle_Player")]
public class PlayerIdle : PlayerGroundedState
{

    public override void Enter()
    {
        base.Enter();
        //停止移动
        movementSpeedModifier=0f;
        SetZeroVelocity();
    }

    public override void LogicUpdate()
    {
        if(PlayerInput.Instance.IsAttack)
        {
            playerStateMachine.ChangeState(typeof(PlayerLightSwordAttackState));
        }
        if(canJump)
        {
            playerStateMachine.ChangeState(typeof(PlayerJumpUp));
        }
        if(PlayerInput.Instance.IsDefense)
        {
            playerStateMachine.ChangeState(typeof(PlayerDefenseStartState));
        }
        //如果速度为0，返回
        if(MoveVector2==Vector2.zero)
        {
            return;
        }

        ChangeToMove();
    }

    private void ChangeToMove()
    {
        if(canWalk)
        {
            playerStateMachine.ChangeState(typeof(PlayerWalk));
        }
        
        playerStateMachine.ChangeState(typeof(PlayerRunStart));
    }
}
