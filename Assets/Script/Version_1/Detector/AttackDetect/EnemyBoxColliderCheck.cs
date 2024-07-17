using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 敌人受击检测
/// </summary>
public class EnemyBoxColliderCheck : MonoBehaviour
{
    PlayerControl playerControl;
    // 定义一个委托类型，用于传递攻击信息
    public delegate void EnemyAttackEventHandler(AttackInfo info);
    
    // 定义一个事件，使用委托类型
    public static event EnemyAttackEventHandler OnAttackReceived;
    public AttackInfo attackInfo;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerControl=GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PlayerAttackCollider")
        {
            var weapon=other.GetComponent<Weapon>();
            attackInfo=new AttackInfo(weapon.baseAttack,weapon.weaponType);
            // 触发事件，传递攻击信息
            OnAttackReceived?.Invoke(attackInfo);
        }
    }
}
}