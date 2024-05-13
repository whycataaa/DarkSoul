using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器接口
/// </summary>
interface IWeapon
{
    //武器名
    string weaponName{get;}
    //武器ID
    int WeaponID{get;}
    WeaponType weaponType{get;}
    //武器基础攻击力
    float BaseAttack{get;}

    void Use();
}
