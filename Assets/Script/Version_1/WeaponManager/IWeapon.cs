using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 武器接口
/// </summary>
interface IWeapon
{
    //武器名
    string weaponName{get;set;}
    //武器ID
    int weaponID{get;set;}
    WeaponType weaponType{get;set;}
    //武器基础攻击力
    float baseAttack{get;set;}

    void Use();
}
