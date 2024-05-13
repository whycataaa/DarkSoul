using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 近战武器使用盒碰撞器检测
/// </summary>
public class PlayerWeaponBoxColliderCheck : MonoBehaviour
{

    public EnemyDetector enemyDetector;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        enemyDetector=GameObject.Find("PlayerControl").GetComponentInChildren<EnemyDetector>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            Debug.Log(other.name);
            if(enemyDetector.enemyTable.ContainsKey(other.name))
            {
                enemyDetector.enemyTable[other.name].IsHit=true;
            }
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag=="Enemy")
        {
            if(enemyDetector.enemyTable.ContainsKey(other.name))
            {
                enemyDetector.enemyTable[other.name].IsHit=false;
            }
        }
    }


}
