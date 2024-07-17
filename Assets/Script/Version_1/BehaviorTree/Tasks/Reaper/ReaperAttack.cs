using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using game2;
namespace BehaviorDesigner.Runtime.Tasks
{



    /// <summary>
    /// 近战攻击
    /// </summary>
    public class ReaperAttack : Attack
    {
        public int[] rate=new int[3]{20,20,30};
        public int[] rate2=new int[6]{10,10,10,20,20,30};


        public override void OnStart()
        {

            //一阶段普通三连击
            var x=GetRandPersonalityType(rate,100);
            //二阶段多了三个combo
            if(enemyControl.currentHp/enemyControl.maxHp*100<=50)
            {
                x=GetRandPersonalityType(rate2,100);
            }

            animator.SetInteger("attackModel",x+1);
            animator.SetTrigger("attack");

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