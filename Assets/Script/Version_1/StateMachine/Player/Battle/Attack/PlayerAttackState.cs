using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
public class PlayerAttackState : PlayerBattleState
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
        playerController.CanRecoverStamina=true;
    }
    public override void HandleAttackReceived(AttackInfo info)
    {
        base.HandleAttackReceived(info);
    }
}
}