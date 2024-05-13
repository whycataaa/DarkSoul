using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 敌人控制类
/// </summary>
public class EnemyControl : CharacterControl
{

    public BoxCollider2D weaponCollider;
    public GameObject enemy;

    public bool CanDamage
    {
        set
        {
            if(value)
            {
                weaponCollider.enabled=true;
            }
            else
            {
                weaponCollider.enabled=false;
            }
        }
    }

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        currentHp=maxHp;
        IsHit=false;
        enemy=transform.GetChild(0).gameObject;
        animator=enemy.GetComponent<Animator>();
        rb=enemy.GetComponent<Rigidbody>();
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(currentHp<=0)
        {
            animator.SetTrigger("IsDie");
        }
    }


    protected override void BeHit()
    {
        currentHp-=10;
        Debug.Log("EnemyCurrentHP:"+currentHp);
        animator.SetTrigger("IsHit");
    }
}
