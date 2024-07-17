using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace game2{
public class Reaper : EnemyControl
{
    public override float CurrentHp
    {
        get => currentHp ;

        set
        {
            float oldValue = currentHp;
            currentHp = value;

            //血量减少进入受击
            if(currentHp<oldValue)
            {
                animator.SetTrigger("IsHit");
                if(currentHp<=0)
                {
                    animator.SetTrigger("IsDie");
                    GameMgr.Instance.OpenWin();
                }
            }
            if(value!=oldValue)
            {
                UIControl.Instance.enemy_Base.FillEnemyHPBar(value,maxHp,enemyID);
            }
        }

    }
    //激光预制体和位置
    public GameObject shootPrefab;
    public Transform shootTrans;
    //冲击波预制体和位置
    public GameObject wavePrefab;
    public Transform waveTrans;


    public override void GetDamage(AttackInfo info)
    {
        CurrentHp-=10;
        animator.Play("getHit");
        Debug.Log("EnemyCurrentHP:"+CurrentHp);
    }
    public void CreateWave()
    {
        Instantiate(wavePrefab,waveTrans);
    }

    public void Shoot()
    {
        Instantiate(shootPrefab,shootTrans);
    }
}
}