using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;

namespace PolygonProject
{
    [CreateAssetMenu(menuName ="Data/Polygon/JumpStart",fileName ="JumpStart_Player")]
    public class PlayerJumpStart : PlayerState
    {
        //跳跃的最大时间
        [SerializeField]float jumpMaxTime;
        //跳跃的最小时间
        [SerializeField]float jumpFixTime;
        //跳跃的速度曲线
        [SerializeField]AnimationCurve speedCurve;
        public override void Enter()
        {
            base.Enter();
            playerControl.SetZeroVelocity();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            //在达到最小跳跃高度前
            if(jumpFixTime>stateDuration)
            {
                return;
            }
            if(jumpMaxTime<stateDuration||!PlayerInputHandler.Instance.IsJump||(playerControl.Rb.velocity.y<=0&&!playerControl.IsGround)||IsAnimationFinished)
            {
                playerStateMachine.ChangeState(typeof(PlayerJumpLoop));
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
