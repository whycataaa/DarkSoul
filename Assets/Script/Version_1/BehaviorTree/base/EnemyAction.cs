using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;
namespace game2{
public class EnemyAction : Action
{
    protected Rigidbody rb;
    protected Animator animator;
    protected PlayerControl playerControl;
    protected Transform playerTrans;
    protected EnemyControl enemyControl;
    protected GameObject enemy;
    public override void OnAwake()
    {
        enemy=gameObject;
        enemyControl=transform.parent.GetComponent<EnemyControl>();
        rb=enemy.GetComponent<Rigidbody>();
        animator=enemy.GetComponent<Animator>();
        playerControl=GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
        playerTrans=GameObject.Find("PlayerControl/Player").transform;
    }
}
}