using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 受击状态
/// </summary>
public class PlayerHitState : PlayerBattleState
{
    public override void Enter()
    {
        base.Enter();
        playerController.CanRecoverStamina=false;
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
        playerController.CanRecoverStamina=true;
    }
    public override void HandleAttackReceived(AttackInfo info)
    {
        base.HandleAttackReceived(info);
    }
}
}