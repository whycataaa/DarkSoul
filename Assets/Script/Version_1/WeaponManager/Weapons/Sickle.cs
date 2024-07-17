using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 镰刀
/// </summary>
[CreateAssetMenu(menuName ="Data/Weapon/Sickle",fileName ="Sickle")]
public class Sickle : Weapon
{

    public override void Use()
    {
        Debug.Log($"{GetType().Name}被使用，造成了{baseAttack}点伤害。");
    }
}
}