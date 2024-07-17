using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

[TaskDescription("检测与玩家的距离，若与玩家距离在A和B之间返回true，反之返回false")]
public class DistanceCheck : EnemyConditional
{
    public float distanceA;
    public float distanceB;
    public override TaskStatus OnUpdate()
    {
        float currentDistance = Vector3.Distance(transform.position, playerTrans.position);
        if (currentDistance > distanceA&&currentDistance<distanceB)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failure;
        }
    }
}
