using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Hit/LightHit_Player",fileName ="LightHit_Player")]
public class PlayerLightHitState : PlayerHitState
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