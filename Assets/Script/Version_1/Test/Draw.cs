using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public float r;

    private void OnDrawGizmos() {
        Gizmos.DrawSphere(this.transform.position,r);
        // 获取角色中心位置
        Vector3 center = transform.position;

        // 向上绘制直线
        Debug.DrawLine(center, center + Vector3.forward * lineLength, Color.red);
        // 向下绘制直线
        Debug.DrawLine(center, center + Vector3.back * lineLength, Color.green);
        // 向左绘制直线
        Debug.DrawLine(center, center + Vector3.left * lineLength, Color.blue);
        // 向右绘制直线
        Debug.DrawLine(center, center + Vector3.right * lineLength, Color.yellow);
    }
    public float lineLength = 5f;

    void Update()
    {
        // 获取角色中心位置
        Vector3 center = transform.position;

        // 向上绘制直线
        Debug.DrawLine(center, center + Vector3.forward * lineLength, Color.red);
        // 向下绘制直线
        Debug.DrawLine(center, center + Vector3.back * lineLength, Color.green);
        // 向左绘制直线
        Debug.DrawLine(center, center + Vector3.left * lineLength, Color.blue);
        // 向右绘制直线
        Debug.DrawLine(center, center + Vector3.right * lineLength, Color.yellow);
    }
}
