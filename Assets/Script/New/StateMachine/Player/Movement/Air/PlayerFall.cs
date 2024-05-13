using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 下落状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Fall",fileName ="Fall_Player")]
public class PlayerFall : PlayerAirState
{
    [SerializeField]AnimationCurve speedCurve;




    public override void Enter()
    {
        base.Enter();
        movementSpeedModifier=0.3f;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        //落地
        if(playerController.IsGround)
        {
            playerStateMachine.ChangeState(typeof(PlayerLand));
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
        playerController.SetVelocityY(speedCurve.Evaluate(stateDuration));
    }
}
