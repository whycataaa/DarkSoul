using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookAtTarget : MonoBehaviour
{
    public Transform targetTrans;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        targetTrans=GameObject.Find("Player").transform;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        // var pos=new Vector3(targetTrans.position.x,this.transform.position.y,targetTrans.position.z);
        // transform.LookAt(pos);

        var direction = targetTrans.position - transform.position;
        direction.y = 0; //如果不考虑y轴，水平跑走的话
        direction.Normalize();
        transform.forward = -direction;
    }
}
