using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
public class PlayerGroundedState : PlayerMovementState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(PlayerInput.Instance.IsJump)
        {
            playerStateMachine.ChangeState(typeof(PlayerJumpUp));
        }
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
        if(PlayerInput.Instance.IsDefense)
        {
            playerStateMachine.ChangeState(typeof(PlayerDefenseStartState));
        }
        if(PlayerInput.Instance.IsRoll)
        {
            playerStateMachine.ChangeState(typeof(PlayerRollState));
        }
        if(MoveVector2==Vector2.zero)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
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