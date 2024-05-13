using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSword : Weapon
{
    public override string weaponName => name;

    public override int WeaponID => 1;

    public override float BaseAttack => 10f;

    public override WeaponType weaponType => WeaponType.Light;

    public override void Use()
    {
        Debug.Log($"{GetType().Name}被使用，造成了{BaseAttack}点伤害。");
    }
}
