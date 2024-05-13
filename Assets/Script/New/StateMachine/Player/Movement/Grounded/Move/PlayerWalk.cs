using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Walk",fileName ="Walk_Player")]
public class PlayerWalk : PlayerGroundedState
{
    [Header("行走速率")]
    [SerializeField]float walkSpeedRate;

    public override void Enter()
    {
        base.Enter();
        movementSpeedModifier=walkSpeedRate;
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();

        if(!canWalk)
        {
            playerStateMachine.ChangeState(typeof(PlayerRunStart));
        }
    }



}
