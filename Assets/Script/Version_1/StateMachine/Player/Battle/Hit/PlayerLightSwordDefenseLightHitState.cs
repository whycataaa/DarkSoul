using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 持轻剑受轻击
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Battle/LightSwordDefenseHit_Player",fileName ="LightSwordDefenseLightLightHit_Player")]
public class PlayerLightSwordDefenseLightHitState : PlayerBattleState
{

    public override void Enter()
    {
        base.Enter();
    }

    public override void LogicUpdate()
    {
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }
    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();
    }
    public override void HandleAttackReceived(AttackInfo info)
    {

    }
}
}