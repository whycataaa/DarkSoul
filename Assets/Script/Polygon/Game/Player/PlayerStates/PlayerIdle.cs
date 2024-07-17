using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PolygonProject
{
    [CreateAssetMenu(menuName ="Data/Polygon/Idle",fileName ="Idle_Player")]
    public class PlayerIdle : PlayerState
    {
        public override void Enter()
        {
            base.Enter();
            playerControl.SetZeroVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(PlayerInputHandler.Instance.IsAttack)
            {
                playerStateMachine.ChangeState(typeof(PlayerUseWeaponR));
            }
            if(PlayerInputHandler.Instance.IsDefense)
            {
                playerStateMachine.ChangeState(typeof(PlayerUseWeaponL));
            }
            if(PlayerInputHandler.Instance.MoveAmount!=0)
            {
                playerStateMachine.ChangeState(typeof(PlayerMove));
            }
            if(PlayerInputHandler.Instance.IsRoll)
            {
                playerStateMachine.ChangeState(typeof(PlayerRoll));
            }
            if(PlayerInputHandler.Instance.IsJump)
            {
                playerStateMachine.ChangeState(typeof(PlayerJumpStart));
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
    }
}
