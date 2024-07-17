using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 青龙刀
/// </summary>
[CreateAssetMenu(menuName = "Data/Weapon/Falchion", fileName = "Falchion")]
public class Falchion : Weapon
{

    public override void Use()
    {
        Debug.Log($"{GetType().Name}被使用，造成了{baseAttack}点伤害。");
    }
}
}