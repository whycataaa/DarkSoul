using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Sprint",fileName ="Sprint_Player")]
public class PlayerSprint : PlayerGroundedState
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
        base.HandleAttackReceived(info);
    }
}
}