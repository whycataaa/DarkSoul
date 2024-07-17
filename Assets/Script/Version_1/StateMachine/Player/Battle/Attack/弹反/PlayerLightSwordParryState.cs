using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 轻剑弹反状态
/// </summary>

[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Battle/LightSwordParry_Player",fileName ="LightSwordParry_Player")]
public class PlayerLightSwordParryState : PlayerAttackState
{

    [SerializeField]float attackStamina;
    public override void Enter()
    {
        base.Enter();
        playerController.CurrentStamina-=attackStamina;
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