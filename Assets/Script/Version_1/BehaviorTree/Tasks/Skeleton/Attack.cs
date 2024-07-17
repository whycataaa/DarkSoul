using System.Collections;
using System.Collections.Generic;
using game2;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{

    public class Attack : EnemyAction
    {
        public string animName;
        protected float stateStartTime;
        protected float stateDuration=>Time.time-stateStartTime;
        protected bool IsAnimationFinished=>stateDuration>=animator.GetCurrentAnimatorStateInfo(0).length;

        public override void OnStart()
        {
            stateStartTime=Time.time;
            animator.SetBool("IsAttack",true);
            rb.velocity=Vector3.zero;
        }
        public override TaskStatus OnUpdate()
        {
            //动画结束返回success
            if(IsAnimationFinished)
            {
                animator.SetBool("IsAttack",false);

                return TaskStatus.Success;
            }

            return TaskStatus.Running;
        }

    }
}