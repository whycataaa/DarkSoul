using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattleState : PlayerState
{



    public override void LogicUpdate()
    {
        //动画结束
        if(!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            Debug.Log("finish state");
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
    }
}
