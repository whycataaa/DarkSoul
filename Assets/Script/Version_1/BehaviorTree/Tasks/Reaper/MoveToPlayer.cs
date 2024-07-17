using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;

namespace game2{
public class MoveToPlayer : EnemyAction
{
    public float targetDistance;
    public float moveSpeed;

    private NavMeshAgent agent;
    Vector3 directionToPlayer;

    public override void OnStart()
    {
        base.OnStart();
        agent=GetComponent<NavMeshAgent>();
        animator.SetTrigger("forward");
        agent.speed=moveSpeed;
    }
    public override TaskStatus OnUpdate()
    {
        // 计算敌人移动方向向量
        directionToPlayer = playerTrans.position - transform.position;


        if (directionToPlayer.magnitude <= targetDistance)
        {
            animator.CrossFade("idle",0.2f);
            return TaskStatus.Success;
        }



        return TaskStatus.Running;
    }
    public override void OnFixedUpdate()
    {
        //朝向玩家
        var pos=playerTrans.position;
        pos.y=transform.position.y;
        rb.transform.LookAt(pos);



        // 如果敌人和玩家之间的距离大于停止距离，移动敌人
        if (directionToPlayer.magnitude > targetDistance)
        {
            agent.SetDestination(playerTrans.position);
        }
        else
        {
            // 如果敌人和玩家之间的距离小于等于停止距离，停止移动
            rb.velocity = Vector3.zero;
        }
    }

}
}