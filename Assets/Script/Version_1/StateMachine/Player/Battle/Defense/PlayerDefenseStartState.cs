using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 防御前摇状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Attack/DefenseStart_Player",fileName ="DefenseStart_Player")]
public class PlayerDefenseStartState : PlayerBattleState
{

    public override void Enter()
    {
        base.Enter();
        SetZeroVelocity();
    }

    public override void LogicUpdate()
    {
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerLightSwordDefenseState));
        }
    }
    public override void PhysicUpdate()
    {
        
    }
    public override void Exit()
    {
        
    }
    public override void HandleAttackReceived(AttackInfo info)
    {

    }
}
}