using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
public class PlayerAirState : PlayerMovementState
{
    public override void Enter()
    {
        base.Enter();
    }
    public override void LogicUpdate()
    {

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