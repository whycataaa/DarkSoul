using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorDesigner.Runtime.Tasks
{
    public class FanDetection : EnemyConditional
    {
        public float radius = 5f; // 扇形半径
        public float angle = 90f; // 扇形角度
        public int rayCount = 30; // 射线数量
        public LayerMask targetLayer; // 目标层
        public Transform detectTrans;//发出射线点
        public string TargetName;//检测目标名称
        public override TaskStatus OnUpdate()
        {
            // 计算扇形的起始角度和结束角度
            float halfAngle = angle / 2;
            float startAngle = -halfAngle;
            float endAngle = halfAngle;

            // 计算每条射线的角度增量
            float angleStep = angle / rayCount;

            for (int i = 0; i <= rayCount; i++)
            {
                // 计算当前射线的角度
                float currentAngle = startAngle + i * angleStep;

                // 将当前角度转换为方向向量
                Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * detectTrans.forward;

                // 发射射线
                RaycastHit hit;
                if (Physics.Raycast(detectTrans.position, direction, out hit, radius, targetLayer))
                {
                    if(hit.collider.name==TargetName)
                    {
                        return TaskStatus.Success;
                    }
                    Debug.Log(hit.collider.name + " is in the sector.");
                }

                // 在编辑器中可视化射线
                Debug.DrawRay(detectTrans.position, direction * radius, Color.green);

            }
            return TaskStatus.Running;
        }


        public override void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(detectTrans.position, radius);

            Vector3 forward = detectTrans.forward * radius;
            Vector3 rightBoundary = Quaternion.Euler(0, angle / 2, 0) * forward;
            Vector3 leftBoundary = Quaternion.Euler(0, -angle / 2, 0) * forward;

            Gizmos.color = Color.blue;
            Gizmos.DrawLine(detectTrans.position, detectTrans.position + rightBoundary);
            Gizmos.DrawLine(detectTrans.position, detectTrans.position + leftBoundary);
        }

    }
}