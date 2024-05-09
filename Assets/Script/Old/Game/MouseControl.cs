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
        //��������ڴ�������
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
    /// ��ʾ���
    /// </summary>
    public void ShowMouse()
    {
        Cursor.visible = true;
    }
    /// <summary>
    /// �������
    /// </summary>
    public void DisShowMouse()
    {
        Cursor.visible = false;
    }
}
