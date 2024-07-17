using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using game2;
public class Death : EnemyAction
{


    public override void OnStart()
    {
        animator.SetBool("IsDie",true);
        enemy.GetComponent<BehaviorTree>().enabled=false;
    }
}
