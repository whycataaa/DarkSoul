using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 黑剑
/// </summary>
[CreateAssetMenu(menuName ="Data/Weapon/DarkSword",fileName ="DarkSword")]
public class DarkSword : Weapon
{

    public override void Use()
    {
        Debug.Log($"{GetType().Name}被使用，造成了{baseAttack}点伤害。");
    }

}
}