using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 退出游戏界面
/// </summary>
public class Panel_ExitView : MonoBehaviour
{
    [SerializeField] private string s_Scene;//要跳转的场景名词，如果为空则退出游戏
    [SerializeField] Button bt_Yes, bt_No;

    private void Start()
    {
        bt_Yes.onClick.AddListener(delegate () { OnButtonClick(bt_Yes); });
        bt_No.onClick.AddListener(delegate () { OnButtonClick(bt_No); });
    }

    //点击按钮事件
    void OnButtonClick(Button btn)
    {
        if (btn == bt_Yes)//点击确定
        {
            Time.timeScale = 1.0f;
            if (s_Scene == "" || s_Scene == null) { Application.Quit(); }
            else { SceneManager.LoadScene(s_Scene); }
        }
        else if (btn == bt_No)//点击取消
        {
            Time.timeScale = 1.0f;
            GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
            viewController.SetPanelActive(viewController.panel_GameMain, null);
        }
    }
}