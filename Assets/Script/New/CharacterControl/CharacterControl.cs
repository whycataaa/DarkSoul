using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色控制类的基类
/// </summary>
public class CharacterControl : MonoBehaviour
{
    [HideInInspector]public Rigidbody rb;
    [HideInInspector]public Animator animator;
    ////////////////////////////////////////////////////////////////////////////////
    ///   战斗相关
    ////////////////////////////////////////////////////////////////////////////////
    //开始前摇
    public bool IsPreAttack=false;

    //能否连击
    public bool CanCombo=false;
    //能否转换动画
    public bool CanSwitch=false;
    //是否受击
    private bool isHit;
    public bool IsHit
    {
        get
        {
            return isHit;
        }
        set
        {
            if(value!=isHit)
            {
                isHit=value;
                if(isHit)
                {
                    BeHit();
                }
            }
        }
    }

    //角色最大血量
    public float maxHp;
    //角色当前血量
    public float currentHp;
    protected virtual void BeHit()
    {
        Debug.Log(this.name+"Be Hit");
    }

}
