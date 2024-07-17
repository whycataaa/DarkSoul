using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityCharacterController;
using UnityEngine;
namespace game2{
/// <summary>
/// 跑步状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Run",fileName ="Run_Player")]
public class PlayerRun : PlayerGroundedState
{
    public override void Enter()
    {
        base.Enter();
        movementSpeedModifier=1f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(canWalk)
        {
            playerStateMachine.ChangeState(typeof(PlayerWalk));
        }
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
}
}