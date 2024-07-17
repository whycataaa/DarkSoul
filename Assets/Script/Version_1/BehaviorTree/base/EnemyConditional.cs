using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
using game2;
public class EnemyConditional : Conditional
{
    protected Rigidbody rb;
    protected Animator animator;
    protected PlayerControl playerControl;
    protected Transform playerTrans;
    protected EnemyControl enemyControl;
    public override void OnAwake()
    {
        rb=GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
        playerControl=GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
        playerTrans=GameObject.Find("PlayerControl/Player").transform;
        enemyControl=transform.parent.GetComponent<EnemyControl>();
    }
}
