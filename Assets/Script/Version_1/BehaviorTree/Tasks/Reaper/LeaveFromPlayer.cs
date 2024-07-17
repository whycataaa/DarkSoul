using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.AI;
namespace game2{
public class LeaveFromPlayer : EnemyAction
{
    bool a=true;
    public float moveSpeed;
    public float targetDistance;
    private NavMeshAgent agent;
    Vector3 directionToPlayer;
    public override void OnStart()
    {
        rb.velocity=Vector3.zero;
        agent=GetComponent<NavMeshAgent>();
        //不转向
        agent.updateRotation = false;
        agent.speed=moveSpeed;
        animator.CrossFade("floatBackwards",0.1f);

    }

    public override TaskStatus OnUpdate()
    {
        if(a)
        {
            agent.SetDestination(-directionToPlayer+targetDistance*Vector3.forward);
            // 计算敌人移动方向向量
            directionToPlayer = playerTrans.position - transform.position;

            if (directionToPlayer.magnitude >= targetDistance)
            {
                animator.CrossFade("idle",0.2f);
                a=false;
                rb.velocity=Vector3.zero;
                return TaskStatus.Success;
            }

                return TaskStatus.Running;


        }
        return TaskStatus.Failure;
    }
    public override void OnFixedUpdate()
    {
        //朝向玩家
        var pos=playerTrans.position;
        pos.y=transform.position.y;
        rb.transform.LookAt(pos);

    }
}
}