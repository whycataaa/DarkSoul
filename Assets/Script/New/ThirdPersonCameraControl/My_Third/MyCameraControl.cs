using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 学习相机控制的脚本（useless）
/// </summary>
public class MyCameraControl : Singleton<CameraControl>
{
    [Header("横向节点")]
    [SerializeField]private GameObject _hNode;
    [Header("纵向节点")]
    [SerializeField]private GameObject _vNode;
    [Header("环绕速度")]
    public float xSpeed = 0.01f;
    [Header("垂直速度")]
    public float ySpeed = 0.01f;
    [Header("X向旋转")]
    [SerializeField]private float _rot_x = 0f;
    [Header("Y向旋转")]
    [SerializeField]private float _rot_y = 0f;

    [Header("Y向旋转上限")]
    public float rotYMax = 70f;
    [Header("Y向旋转下限")]
    public float rotYMin = 290f;


    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        //鼠标固定在屏幕中心
        Cursor.lockState=CursorLockMode.Locked;
        CameraStart();
    }



    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        CursorSet();

    }

    /// <summary>
    /// 对相机操作，写在FixedUpdate防止相机抖动
    /// </summary>
    void FixedUpdate()
    {
        //鼠标显示后不会影响摄像机
        if (!PlayerInput.Instance.IsShowCursor)
        {

            // 获取鼠标输入
            float inputX = Input.GetAxis("Mouse X");
            float inputY = Input.GetAxis("Mouse Y");
            // 将鼠标输入乘以速度
            float changeX = inputX * xSpeed;
            float changeY = -inputY * ySpeed;
            // 获取v节点的本地旋转
            Vector3 vRotation = _vNode.transform.localEulerAngles;
            // 限制相机视角上下旋转
            if (vRotation.x < 180 && vRotation.x + changeY > rotYMax)
            {
                changeY = rotYMax - vRotation.x;
            }
            else if (vRotation.x > 180 && vRotation.x + changeY < rotYMin)
            {
                changeY = rotYMin - vRotation.x;
            }

            // 旋转控制节点
            _vNode.transform.Rotate(changeY, 0, 0);
            _hNode.transform.Rotate(0, changeX, 0);
        }

    }
    /// <summary>
    /// 鼠标锁定隐藏以及不锁定时显示
    /// </summary>
    private static void CursorSet()
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

    /// <summary>
    /// 相机初始化
    /// </summary>
    private void CameraStart()
    {
        // 初始化节点旋转
        _hNode.transform.localRotation = Quaternion.identity;
        _vNode.transform.localRotation = Quaternion.identity;
        // 先横向旋转
        _hNode.transform.Rotate(0, _rot_x, 0);
        // 再纵向旋转
        _vNode.transform.Rotate(_rot_y, 0, 0);
    }
}
