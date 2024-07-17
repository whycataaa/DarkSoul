using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerSprint : PlayerState
    {
        [SerializeField]float Speed;
        [SerializeField]float RotationSpeed;
        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(!playerControl.IsGround)
            {
                playerStateMachine.ChangeState(typeof(PlayerJumpLoop));
            }
            if(PlayerInputHandler.Instance.MoveAmount==0)
            {
                playerStateMachine.ChangeState(typeof(PlayerIdle));
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
            playerControl.Move(Speed,RotationSpeed);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
