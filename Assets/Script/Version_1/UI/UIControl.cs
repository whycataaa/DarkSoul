using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
public class UIControl : SingletonMono<UIControl>
{
    public Panel_Enemy_Base enemy_Base;
    public Panel_Player_Base player_Base;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        enemy_Base=GameObject.Find("CanvasMain/Panel_EnemyBase_Logic").GetComponent<Panel_Enemy_Base>();
        player_Base=GameObject.Find("CanvasMain/Panel_PlayerBase_Logic").GetComponent<Panel_Player_Base>();
    }



    /// <summary>
    /// 生成怪物血条
    /// </summary>
    /// <param name="enemyType">敌人类型</param>
    public GameObject InitializeHp(EnemyType enemyType)
    {
        switch(enemyType)
        {
            case EnemyType.Boss:
                return Instantiate(enemy_Base.panel_Enemy_BossBasePrefab,enemy_Base.transform);

            case EnemyType.Normal:
                return Instantiate(enemy_Base.panel_Enemy_NormalBasePrefab,enemy_Base.transform);
        }

        return null;
    }

    public GameObject InitializeHp(EnemyType enemyType,Transform trans)
    {
        switch(enemyType)
        {
            case EnemyType.Boss:
                return Instantiate(enemy_Base.panel_Enemy_BossBasePrefab,enemy_Base.transform);

            case EnemyType.Normal:
                return Instantiate(enemy_Base.panel_Enemy_NormalBasePrefab,trans);
        }

        return null;
    }
}
}