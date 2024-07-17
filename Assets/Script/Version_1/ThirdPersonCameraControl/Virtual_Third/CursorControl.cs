using System.Collections;
using System.Collections.Generic;

using UnityEngine;
namespace game2
{

public class CursorControl : SingletonMono<CursorControl>
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //鼠标固定在屏幕中心
        Cursor.lockState=CursorLockMode.Locked;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CursorSet();

    }



    /// <summary>
    /// 鼠标锁定隐藏以及不锁定时显示
    /// </summary>
    private static void CursorSet()
    {
        if(PlayerInput.Instance!=null)
        {
            switch (PlayerInput.Instance.IsShowCursor)
            {
                case true:
                    Cursor.lockState = CursorLockMode.None;
                    //Debug.Log("鼠标已解除锁定");
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.Locked;
                    //Debug.Log("鼠标已锁定");
                    break;
            }
        }
    }
}
}