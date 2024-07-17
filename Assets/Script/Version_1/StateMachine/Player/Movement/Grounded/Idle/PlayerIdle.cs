using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
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
            switch(playerController.weaponManager.GetCurrentWeaponType())
            {
                case WeaponType.Light:
                    playerStateMachine.ChangeState(typeof(PlayerLightSwordAttackState));
                    break;
                //重剑***
            }
        }
        if(PlayerInput.Instance.IsJump)
        {
            playerStateMachine.ChangeState(typeof(PlayerJumpUp));
        }
        if(PlayerInput.Instance.IsDefense)
        {
            playerStateMachine.ChangeState(typeof(PlayerDefenseStartState));
        }
        if(PlayerInput.Instance.IsRoll)
        {
            playerStateMachine.ChangeState(typeof(PlayerRollState));
        }
        //如果速度为0，返回
        if(MoveVector2==Vector2.zero)
        {
            return;
        }

        ChangeToMove();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public override void Exit()
    {
         base.Exit();
    }
    public override void HandleAttackReceived(AttackInfo info)
    {
        base.HandleAttackReceived(info);
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
}