using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundedState : PlayerMovementState
{
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        if(canJump)
        {
            playerStateMachine.ChangeState(typeof(PlayerJumpUp));
        }
        if(PlayerInput.Instance.IsAttack)
        {
            switch(WeaponManager.Instance.GetCurrentWeaponType())
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
        if(MoveVector2==Vector2.zero)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }
}
