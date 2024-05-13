using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 抽象武器类
/// </summary>
public abstract class Weapon :MonoBehaviour,IWeapon
{
    public abstract string weaponName{get;}

    public abstract int WeaponID{get;}

    public abstract float BaseAttack{get;}

    public abstract WeaponType weaponType{get;}

    public abstract void Use();

}
