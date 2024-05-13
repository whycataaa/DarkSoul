using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家控制类
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerControl : CharacterControl
{

    GroundDetector groundDetector;
    public bool IsGround=>groundDetector.IsGround;
    public BoxCollider weaponCollider;

    GameObject player;
    [HideInInspector]public PlayerInput input;
    [HideInInspector]public Transform playerCameraTrans;
    //玩家状态机
    [HideInInspector]public PlayerStateMachine playerStateMachine;
    ///////////////////////////////////////////
    /// 攻击检测
    ///////////////////////////////////////////

    //能否造成伤害
    public bool CanDamage
    {
        set
        {
            if(value)
            {
                weaponCollider.enabled=true;
            }
            else
            {
                weaponCollider.enabled=false;
            }
        }
    }
    public EnemyDetector enemyDetector;
    //最近敌人位置信息
    public Transform nearestEnemyTrans;
    //最小锁敌距离
    public float MinTrackDistance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        weaponCollider.enabled=false;
        currentHp=maxHp;
        playerCameraTrans=GameObject.Find("PlayerCamera").transform;
        player=GameObject.Find("Player");
        input=GetComponent<PlayerInput>();
        rb=player.GetComponent<Rigidbody>();
        groundDetector=GetComponentInChildren<GroundDetector>();
        animator=player.GetComponent<Animator>();
        enemyDetector=GetComponentInChildren<EnemyDetector>();
        playerStateMachine=GetComponent<PlayerStateMachine>();

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        playerStateMachine.ChangeState(typeof(PlayerIdle));
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        playerStateMachine.LogicUpdate();
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        playerStateMachine.PhysicUpdate();


    }
    //设置Y方向速度
    public void SetVelocityY(float velocityY)
    {
        rb.velocity=new Vector3(rb.velocity.x,velocityY,rb.velocity.z);
    }

    /// <summary>
    /// 获取最近敌人的位置信息
    /// </summary>
    /// <returns>敌人的位置信息</returns>
    public Transform GetNearestEnemyPosition()
    {
        Transform nearestEnemyPosition = null;
        float nearestDistance = MinTrackDistance;

        foreach (var enemyPair in enemyDetector.enemyTable)
        {
            EnemyControl enemy = enemyPair.Value;
            Transform enemyPosition = enemy.transform.GetChild(0).transform; // 假设EnemyControl类有一个获取位置的方法

            // 计算玩家与当前敌人的距离
            float distanceToPlayer = Vector3.Distance(enemyPosition.position, rb.transform.position);

            // 如果当前敌人距离更近，则更新最近敌人的位置信息
            if (distanceToPlayer < nearestDistance)
            {
                nearestDistance = distanceToPlayer;
                nearestEnemyPosition = enemyPosition;
            }
        }

        return nearestEnemyPosition;
    }
    /// <summary>
    /// 转向最近的敌人
    /// </summary>
    public void RotateToEnemy()
    {
        rb.transform.LookAt(nearestEnemyTrans,Vector3.up);
        // // 获取敌人相对于玩家的方向向量
        // Vector3 direction = nearestEnemyTrans.position - rb.transform.position;
        // direction.y = 0; // 只考虑水平方向

        // // 计算玩家需要旋转的角度
        // float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        // // 使用插值函数逐渐调整玩家的旋转角度
        // float angle = Mathf.LerpAngle(rb.transform.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);

        // // 将旋转角度应用到玩家
        // rb.transform.rotation = Quaternion.Euler(0, angle, 0);
        // Debug.Log("has rotate");
        // Debug.Log(rb.transform.rotation);
    }


}
