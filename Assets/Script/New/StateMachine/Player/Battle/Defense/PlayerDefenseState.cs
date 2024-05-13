using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 防御状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Attack/Defense_Player",fileName ="Defense_Player")]
public class PlayerDefenseState : PlayerBattleState
{

    public override void Enter()
    {
        base.Enter();

    }

    public override void LogicUpdate()
    {
        if(!PlayerInput.Instance.IsDefense)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }
}
