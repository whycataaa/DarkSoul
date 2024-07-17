using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace game2{
/// <summary>
/// 抽象武器类
/// </summary>
public abstract class Weapon :ScriptableObject,IWeapon
{
    public GameObject weaponPrefab;
    public string WeaponName;
    public int WeaponID;
    public float BaseAttack;
    public WeaponType WeaponType;
    public string weaponName{get{return weaponName;}set{WeaponName=value;}}
    public int weaponID{get{return WeaponID;}set{WeaponID=value;}}
    public float baseAttack{get{return BaseAttack;}set{BaseAttack=value;}}
    public WeaponType weaponType{get{return WeaponType;}set{WeaponType=value;}}

    public abstract void Use();

}
}