using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
namespace game2{
/// <summary>
/// 玩家控制类
/// </summary>
[RequireComponent(typeof(PlayerInput))]
public class PlayerControl : CharacterControl
{

    #region 地面检测
    [SerializeField]GroundDetector groundDetector;
    public bool IsGround=>groundDetector.IsGround;
    #endregion

    #region 玩家
    public GameObject player;
    public CapsuleCollider cd;
    //玩家状态机
    [HideInInspector]public PlayerStateMachine playerStateMachine;
    #endregion

    #region 相机
    [HideInInspector]public Transform playerCameraTrans;
    public CinemachineVirtualCamera playerCamera;
    private Transform PlayerCameraLookPoint;
    #endregion

    #region 输入
    [HideInInspector]public PlayerInput input;

    #endregion

    #region 收集
    public static int CoinNum { get; set; }
    #endregion

    ///////////////////////////////////////////
    /// 战斗部分
    ///////////////////////////////////////////


    //是否在攻击
    public bool CanRecoverStamina=true;

    //连击的时间
    [HideInInspector]public float ComboTime;
    [Header("最小连击间隔")]
    public float COMBO_TIME;
    //敌人检测
    public EnemyDetector enemyDetector;
    //最近敌人位置信息
    public Transform nearestEnemyTrans;
    //最小锁敌距离
    public float MinTrackDistance;
    //受击力
    public float hitForce;

    public override float CurrentHp
    {

        get=>currentHp;

        set
        {
            float oldValue = currentHp;
            currentHp = value;
            if(oldValue!=value)
            {
                UIControl.Instance.player_Base.FillPlayerHPBar(value,maxHp);
            }
            if(value<=0)
            {
                GameMgr.Instance.OpenDie();
            }

        }
    }
    //体力回复速度
    public float staminaRecoverSpeed;
    public override float CurrentStamina
    {
        get => currentStamina;
        set
        {
            float oldValue = currentStamina;
            currentStamina = value;
            if(oldValue!=value)
            {
                UIControl.Instance.player_Base.FillPlayerStaminaBar(value,maxStamina);
            }
        }
    }

    public Coroutine staminaRecoverCoroutine;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {



    }

    private void SwitchWeapon1(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        weaponManager.SwitchWeapon(0);
    }
    private void SwitchWeapon2(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        weaponManager.SwitchWeapon(1);
    }






    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        #region 组件获取
        playerCamera=GameObject.Find("PlayerCamera").GetComponent<CinemachineVirtualCamera>();
        playerCameraTrans=GameObject.Find("PlayerCamera").transform;
        PlayerCameraLookPoint=GameObject.Find("PlayerControl/Player/CameraLookPoint").transform;
        groundDetector=GameObject.Find("PlayerControl/Player/Detector").GetComponent<GroundDetector>();
        player=GameObject.Find("PlayerControl/Player");
        input=GetComponent<PlayerInput>();
        rb=player.GetComponent<Rigidbody>();
        cd=player.GetComponent<CapsuleCollider>();
        animator=player.GetComponent<Animator>();
        enemyDetector=GetComponentInChildren<EnemyDetector>();
        playerStateMachine=GetComponent<PlayerStateMachine>();
        weaponManager=GetComponent<WeaponManager>();
        #endregion

        #region 按键
        input.gamePlayActions.Num1.started+=SwitchWeapon1;
        input.gamePlayActions.Num2.started+=SwitchWeapon2;
        #endregion

        #region 数据初始化
        CurrentHp=maxHp;
        CurrentStamina=maxStamina;
        ComboTime=COMBO_TIME;

        playerCamera.LookAt=PlayerCameraLookPoint;
        playerCamera.Follow=PlayerCameraLookPoint;


        #endregion

        playerStateMachine.ChangeState(typeof(PlayerIdle));
    }



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(CanRecoverStamina&&IsGround)
        {
            if(CurrentStamina<=maxStamina)
            {
                CurrentStamina=CurrentStamina+staminaRecoverSpeed;
            }
        }
        playerStateMachine.LogicUpdate();
        ComboTime-=Time.deltaTime;

        
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
            // 获取位置
            Transform enemyPosition = enemy.transform.GetChild(0).transform;

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
        if(nearestEnemyTrans)
        {
            Vector3 dir =nearestEnemyTrans.position-rb.transform.position;
            dir=new Vector3(dir.x,0,dir.z);
            rb.transform.rotation=Quaternion.LookRotation(dir);
        }

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
}