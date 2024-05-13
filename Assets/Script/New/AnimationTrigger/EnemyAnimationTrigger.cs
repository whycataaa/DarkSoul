using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationTrigger : MonoBehaviour
{
    public EnemyControl enemyController;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        enemyController=GetComponentInParent<EnemyControl>();
    }

    public void CanDamage()
    {
        enemyController.CanDamage=true;
    }
    public void CannotDamage()
    {
        enemyController.CanDamage=false;
    }


}
