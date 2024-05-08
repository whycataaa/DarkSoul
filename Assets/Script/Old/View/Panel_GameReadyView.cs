using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏初始化完毕，等待开始游戏界面
/// </summary>
public class Panel_GameReadyView : MonoBehaviour
{
    [SerializeField] private string s_Scene;

    //游戏开始按钮
    public void OnButtonStart()
    {
        SceneManager.LoadScene(s_Scene);
    }
}