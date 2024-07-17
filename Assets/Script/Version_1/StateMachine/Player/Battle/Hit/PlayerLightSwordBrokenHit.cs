using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 轻剑破防
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Hit/LightSwordBrokenHit_Player",fileName ="LightSwordBrokenHit")]
public class PlayerLightSwordBrokenHit : PlayerHitState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {
        base.LogicUpdate();
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
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