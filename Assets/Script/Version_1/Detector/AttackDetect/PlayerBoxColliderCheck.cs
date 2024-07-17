using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
/// <summary>
/// 使用盒碰撞器检测敌人攻击
/// </summary>
public class PlayerBoxColliderCheck : MonoBehaviour
{
    // 定义一个委托类型，用于传递攻击信息
    public delegate void PlayerAttackEventHandler(AttackInfo info);
    
    // 定义一个事件，使用委托类型
    public static event PlayerAttackEventHandler OnAttackReceived;
    public EnemyDetector enemyDetector;
    public AttackInfo attackInfo;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        //enemyDetector=GameObject.Find("PlayerControl").GetComponentInChildren<EnemyDetector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="EnemyAttackCollider")
        {
            var weapon=other.GetComponent<Weapon>();
            attackInfo=new AttackInfo(weapon.baseAttack,weapon.weaponType);
            // 触发事件，传递攻击信息
            OnAttackReceived?.Invoke(attackInfo);
        }
    }


}
}