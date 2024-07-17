using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PolygonProject
{
    [CreateAssetMenu(menuName ="Data/Polygon/JumpEnd",fileName ="JumpEnd_Player")]
    public class PlayerJumpEnd : PlayerState
    {
        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(IsAnimationFinished)
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
    }
}
