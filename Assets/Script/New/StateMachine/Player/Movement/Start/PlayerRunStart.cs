using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/RunStart",fileName ="RunStart_Player")]
public class PlayerRunStart : PlayerMovementState
{
    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(MoveVector2==Vector2.zero)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerRun));
        }
    }

}
