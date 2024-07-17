using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Roll",fileName ="Roll_Player")]
public class PlayerRollState : PlayerState
{

    [SerializeField]float rollForce;
    [SerializeField]float rollStamina;
    public override void Enter()
    {
        base.Enter();
        playerController.CanRecoverStamina=false;
        playerController.CurrentStamina-=rollStamina;
        playerController.player.tag="Untagged";
        playerController.rb.AddForce(playerController.player.transform.forward*rollForce,ForceMode.Impulse);
    }
    public override void LogicUpdate()
    {
        if(IsAnimationFinished)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }
    public override void PhysicUpdate()
    {
        base.PhysicUpdate();
    }
    public override void Exit()
    {
        base.Exit();
        playerController.CanRecoverStamina=true;
        playerController.player.tag="PlayerBody";
    }
    public override void HandleAttackReceived(AttackInfo info)
    {
        base.HandleAttackReceived(info);
    }
}
}