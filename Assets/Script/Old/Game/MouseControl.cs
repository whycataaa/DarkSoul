using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControl : Singleton<MouseControl>
{
    protected override void Awake()
    {
        base.Awake();

    }
    private void Start()
    {
        //鼠标锁定在窗口中心
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if(Cursor.lockState == CursorLockMode.Locked)
            {
                     Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }


        }
    }

    /// <summary>
    /// 显示鼠标
    /// </summary>
    public void ShowMouse()
    {
        Cursor.visible = true;
    }
    /// <summary>
    /// 隐藏鼠标
    /// </summary>
    public void DisShowMouse()
    {
        Cursor.visible = false;
    }
}
