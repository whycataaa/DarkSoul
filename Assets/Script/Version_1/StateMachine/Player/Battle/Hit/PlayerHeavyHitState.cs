using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Hit/HeavyHit_Player",fileName ="HeavyHit_Player")]
public class PlayerHeavyHitState : PlayerHitState
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