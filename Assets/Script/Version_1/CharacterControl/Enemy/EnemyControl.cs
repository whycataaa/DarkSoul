using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace game2{



/// <summary>
/// 敌人控制类
/// </summary>
public class EnemyControl : CharacterControl,IAgent
{
    public int enemyID;
    public EnemyType enemyType;
    public GameObject enemy;
    public BehaviorTree BT;
    public bool CanDamage;
    public override float CurrentHp
    {
        get => currentHp ;

        set
        {
            float oldValue = currentHp;
            currentHp = value;

            //血量减少进入受击
            if(currentHp<oldValue)
            {
                animator.SetTrigger("IsHit");
            }
            if(value!=oldValue)
            {
                UIControl.Instance.enemy_Base.FillEnemyHPBar(value,maxHp,enemyID);
            }
        }

    }

    protected GameObject hpObj;
    private Transform HpTrans;
    private Camera mainCamera;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        mainCamera=GameObject.Find("CameraMain").GetComponent<Camera>();
        weaponManager=GetComponent<WeaponManager>();
        enemy=transform.GetChild(0).gameObject;
        animator=enemy.GetComponent<Animator>();
        rb=enemy.GetComponent<Rigidbody>();
        BT=GetComponentInChildren<BehaviorTree>();
    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        InsHp();

        CurrentHp = maxHp;
    }



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        //UI相机下的血条
        // //小怪血条
        // if(enemyType==EnemyType.Normal)
        // {
        //         var screenPos=mainCamera.WorldToScreenPoint(HpTrans.position);
        //         Camera UICamera=GameObject.Find("CameraUI").GetComponent<Camera>();
        //     //在相机内就显示血条
        //     if (screenPos.z > 0 && screenPos.x > 0 && screenPos.x < Screen.width && screenPos.y > 0 && screenPos.y < Screen.height)
        //     {
        //         hpObj.SetActive(true);

        //         if (UICamera == null)
        //         {
        //             Debug.LogError("UICamera not found!");
        //             return;
        //         }

        //         if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
        //             hpObj.transform.parent as RectTransform,
        //             screenPos, UICamera,
        //             out Vector3 worldPoint))
        //         {
        //             hpObj.transform.position = worldPoint;
        //         }
        //     }
        //     else
        //     {
        //         hpObj.SetActive(false); // Hide the HP bar if the enemy is not in the camera's view
        //     }

        //}
    }

    public virtual void GetDamage(AttackInfo info)
    {
        CurrentHp-=10;
        animator.Play("getHit");
        Debug.Log("EnemyCurrentHP:"+CurrentHp);
    }

    /// <summary>
    /// 血条初始化
    /// </summary>
    private void InsHp()
    {
        //Debug.Log(enemyID);
        switch (enemyType)
        {
            case EnemyType.Normal:
                hpObj = UIControl.Instance.InitializeHp(enemyType,enemy.GetComponentInChildren<Canvas>().transform);
                break;
            case EnemyType.Boss:
                hpObj = UIControl.Instance.InitializeHp(enemyType);
                break;
        }

        hpObj.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text=enemy.name;
        Hp hp;
        hp.healthBar=hpObj.transform.GetChild(3).GetComponent<Image>();
        hp.healthEffect=hpObj.transform.GetChild(2).GetComponent<Image>();
        UIControl.Instance.enemy_Base.hpTable.Add(enemyID,hp);
    }

}

public enum EnemyType
{
    Normal=0,
    Boss=1
}

}