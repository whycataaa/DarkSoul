using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using game2;
namespace game2
{
/// <summary>
/// 法术施放
/// </summary>
public class Cast : EnemyAction
{
    public int[] rate=new int[2]{40,60};


    public string[] animName;
    protected float stateStartTime;
    protected float stateDuration=>Time.time-stateStartTime;
    protected bool IsAnimationFinished=>stateDuration>=animator.GetCurrentAnimatorStateInfo(0).length;


    public override void OnStart()
    {

        var x=GetRandPersonalityType(rate,100);
        animator.SetTrigger(animName[x]);
        stateStartTime=Time.time;
    }

    public override TaskStatus OnUpdate()
    {
        //动画结束返回success
        if(IsAnimationFinished)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;

    }

    private int GetRandPersonalityType(int[] array, int _total)
    {
        int rand = Random.Range(1, _total + 1);
        int tmp = 0;

        for (int i = 0; i < array.Length; i++)
        {
            tmp += array[i];
            if (rand < tmp)
            {
                return i;
            }
        }
        return 0;
    }
}
}