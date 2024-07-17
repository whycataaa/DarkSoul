using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击信息
/// </summary>
public class AttackInfo
{
    public float BaseAttack;
    public WeaponType weaponType;

    public Vector3 attackTrans;
    public AttackInfo(float _baseAttack,WeaponType _weaponType)
    {
        BaseAttack=_baseAttack;
        weaponType=_weaponType;
    }

    public AttackInfo(float _baseAttack)
    {
        BaseAttack=_baseAttack;
    }
}
