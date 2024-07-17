using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/RunStart",fileName ="RunStart_Player")]
public class PlayerRunStart : PlayerMovementState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(MoveVector2==Vector2.zero)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
        if(PlayerInput.Instance.IsJump)
        {
            playerStateMachine.ChangeState(typeof(PlayerJumpUp));
        }
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerRun));
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