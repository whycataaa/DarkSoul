using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 对游戏流程进行管理
/// </summary>
public class GameMgr : SingletonMono<GameMgr>
{
    public GameObject panel_Die;
    public GameObject panel_Win;

    protected override void Awake()
    {

        //加载玩家
     //var go=Instantiate(ABMgr.Instance.LoadRes<GameObject>("battle","PlayerControl"));

       //go.name =  go.name.Replace("(Clone)", "");
      // go.transform.position=Vector3.up;
    }

    public void OpenDie()
    {
        panel_Die.SetActive(true);
    }

    public void OpenWin()
    {
        panel_Win.SetActive(true);
    }
}
