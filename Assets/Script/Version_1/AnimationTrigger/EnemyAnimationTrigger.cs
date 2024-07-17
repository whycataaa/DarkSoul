using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
public class EnemyAnimationTrigger : MonoBehaviour
{
    public EnemyControl enemyController;
    public WeaponManager weaponManager;
    public GameObject player;

    void Awake()
    {
        enemyController=GetComponentInParent<EnemyControl>();
        weaponManager=GetComponentInParent<WeaponManager>();

    }

    public void CanDamage()
    {
        enemyController.CanDamage=true;
        weaponManager.OnDetect=true;
    }
    public void CannotDamage()
    {
        enemyController.CanDamage=false;
        weaponManager.OnDetect=false;
    }

    public void FaceToPlayer()
    {
        var pos=player.transform.position;
        pos.y=transform.position.y;
        enemyController.rb.transform.LookAt(pos);
    }
}
}