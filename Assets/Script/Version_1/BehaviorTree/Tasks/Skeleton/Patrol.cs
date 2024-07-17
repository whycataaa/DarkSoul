using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using game2;
/// <summary>
/// 巡逻
/// </summary>
namespace BehaviorDesigner.Runtime.Tasks
{

    public class Patrol : EnemyAction
    {
        //巡逻范围
        public float PatrolRange;

        //下次巡逻时间
        public float nextPatrolTime;
        //巡逻点
        protected Vector3 patrolPos;
        //导航代理
        private NavMeshAgent agent;
        public Transform detectTrans;
        public override void OnStart()
        {
            agent=GetComponent<NavMeshAgent>();
            animator.SetBool("IsRun",true);
            agent.isStopped=false;
            agent.destination=GetRandomPatrolPos();

        }

        public override TaskStatus OnUpdate()
        {
            if(Vector3.Distance(enemy.transform.position,patrolPos)<=agent.stoppingDistance)
            {
                agent.destination=GetRandomPatrolPos();
            }

            return TaskStatus.Running;
        }

        /// <summary>
        /// 获取随机巡逻点
        /// </summary>
        /// <returns></returns>
        Vector3 GetRandomPatrolPos()
        {
            float randomX=Random.Range(-0.5f*PatrolRange,0.5f*PatrolRange);
            float randomZ=Random.Range(-0.5f*PatrolRange,0.5f*PatrolRange);

            Vector3 randomPos=new Vector3(detectTrans.position.x+randomX,
                                          detectTrans.position.y,
                                          detectTrans.position.z+randomZ);
            NavMeshHit navMeshHit;
            patrolPos=NavMesh.SamplePosition(randomPos,out navMeshHit,PatrolRange,NavMesh.AllAreas)
                        ?navMeshHit.position:transform.position;

            return patrolPos;
        }



#if UNITY_EDITOR

        public override void OnDrawGizmos()
        {

            Gizmos.color=Color.yellow;

            Gizmos.DrawWireSphere(detectTrans.position,PatrolRange);

        }

        #endif
    }

}