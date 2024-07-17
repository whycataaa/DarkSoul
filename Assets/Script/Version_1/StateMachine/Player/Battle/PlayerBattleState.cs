using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
public class PlayerBattleState : PlayerState
{

    public override void Enter()
    {
        base.Enter();
    }
    /// <summary>
    /// 动画结束进入Idle
    /// </summary>
    public override void LogicUpdate()
    {
        //动画结束
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
        base.HandleAttackReceived(info);
    }

}
}