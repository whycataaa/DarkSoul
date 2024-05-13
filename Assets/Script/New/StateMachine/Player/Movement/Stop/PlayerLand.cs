using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Land",fileName ="Land_Player")]
public class PlayerLand : PlayerGroundedState
{

    public float coolDownTime;
    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        //没到冷却时间（在僵直）
        if(stateDuration<coolDownTime)
        {
            return;
        }
        //僵直结束直接按方向键进入移动
        if(MoveVector2!=Vector2.zero)
        {
            playerStateMachine.ChangeState(typeof(PlayerRunStart));
        }
        //僵直结束不动方向
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }

}
