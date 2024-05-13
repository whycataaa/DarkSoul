using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 上升状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/JumpUp",fileName ="JumpUp_Player")]
public class PlayerJumpUp : PlayerAirState
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
        movementSpeedModifier=0.7f;


    }
    public override void LogicUpdate()
    {

        base.LogicUpdate();
    //    Debug.Log(stateDuration);
        //停止跳跃按键立即下落实现长短跳
        //Debug.Log(playerController.rb.velocity.y);
        if(jumpFixTime>stateDuration)
        {
            return;
        }
        if(jumpMaxTime<stateDuration||!PlayerInput.Instance.IsJump||(playerController.rb.velocity.y<=0&&!playerController.IsGround))
        {
            playerStateMachine.ChangeState(typeof(PlayerFall));
        }

    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        playerController.SetVelocityY(speedCurve.Evaluate(stateDuration));

    }
    public override void Exit()
    {
        
    }
}
