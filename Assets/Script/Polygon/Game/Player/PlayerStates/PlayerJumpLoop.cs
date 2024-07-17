using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace PolygonProject
{
    [CreateAssetMenu(menuName ="Data/Polygon/JumpLoop",fileName ="JumpLoop_Player")]
    public class PlayerJumpLoop : PlayerState
    {
        [SerializeField]AnimationCurve speedCurve;
        public override void Enter()
        {
            base.Enter();
            playerControl.SetZeroVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //落地
            if(playerControl.IsGround)
            {
                playerStateMachine.ChangeState(typeof(PlayerJumpEnd));
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
            playerControl.SetVelocityY(speedCurve.Evaluate(stateDuration));
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
