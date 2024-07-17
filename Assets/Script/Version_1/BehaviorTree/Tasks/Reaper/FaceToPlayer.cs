using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using game2;
public class FaceToPlayer : EnemyAction
{


    public override void OnStart()
    {

    }
    public override TaskStatus OnUpdate()
    {
        // Vector3 direction=playerTrans.position-this.transform.position;
        // var pos=playerTrans.position;
        // pos.y=transform.position.y;
        // rb.transform.LookAt(pos);
        // // 计算当前位置到目标位置的夹角
        // float angle = Vector3.Angle(pos, direction);

        // if(angle<0.5)
        // {
        //     return TaskStatus.Success;
        // }

        // return TaskStatus.Running;

         var pos=playerTrans.position;
         pos.y=transform.position.y;
         rb.transform.LookAt(pos);

        // 获取敌人到玩家的方向向量并将其投影到水平平面
        Vector3 enemyDirection = new Vector3(transform.position.x, 0f, transform.position.z);
        Vector3 playerDirection = new Vector3(playerTrans.position.x, 0f, playerTrans.position.z);


        // 计算敌人面朝玩家的角度
        float angle = Vector3.Angle(transform.forward, playerDirection-enemyDirection);

        // 如果角度小于5度，则返回true，否则返回false
        if (angle < 5f)
        {
//            Debug.Log("敌人面朝玩家");
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

}
