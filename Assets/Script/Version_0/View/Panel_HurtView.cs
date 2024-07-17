using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家受伤界面
/// </summary>
public class Panel_HurtView : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Dispare", 3);
    }

    //过几秒后自动消失
    void Dispare()
    {
        GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        viewController.SetPanelActive(viewController.panel_GameMain, null);
    }
}