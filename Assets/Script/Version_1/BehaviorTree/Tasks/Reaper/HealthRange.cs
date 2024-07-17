using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
[TaskDescription("ȷ��������Χ.healthRange����ʾ����Value���µķ���Success��" +
    "���򷵻�Failure;ȡֵ��ʾѪ���İٷֱ�")]
public class HealthRange : EnemyConditional
{
    public float healthRange;
    public override TaskStatus OnUpdate()
    {
        if ((enemyControl.CurrentHp / enemyControl.maxHp)*100 <= healthRange)
            return TaskStatus.Success;
        else
            return TaskStatus.Failure;
    }
}
