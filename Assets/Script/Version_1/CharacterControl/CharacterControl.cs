using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 角色控制类的基类
/// </summary>
public abstract class CharacterControl : MonoBehaviour
{
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public Animator animator;
    ////////////////////////////////////////////////////////////////////////////////
    ///   战斗相关
    ////////////////////////////////////////////////////////////////////////////////
    
    public AttackInfo attackInfo;
    public WeaponManager weaponManager;

    //开始前摇
    public bool IsPreAttack=false;

    //能否连击
    public bool CanCombo=false;
    //能否转换动画
    public bool CanSwitch=false;



    //战斗数值*************



    //角色最大血量
    public float maxHp;

    public float currentHp;
    //角色当前血量
    public virtual float CurrentHp{get;set;}

    //角色最大耐力值
    public float maxStamina;

    //角色当前耐力值
    public float currentStamina;
    public virtual float CurrentStamina{get;set;}

}
}