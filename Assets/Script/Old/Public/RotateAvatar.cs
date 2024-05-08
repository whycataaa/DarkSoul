using System;
using UnityEngine;

public class RotateAvatar : MonoBehaviour 
{
    //枚举旋转类型：X&Y轴同时旋转，X轴旋转，Y轴旋转
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 1f;//水平旋转速率
    public float sensitivityVert = 0f;//垂直旋转速率
    public float minmumVert = -45f;//垂直旋转限制最小角度
    public float maxmumVert = 45f;//垂直旋转最大角度
    private float _rotationX = 0;

    void Update()
    {
        // //按下鼠标右键时旋转。Input.GetMouseButton(0)左键，Input.GetMouseButton(1)右键，Input.GetMouseButton(2)中间键
        // if (!Input.GetMouseButton(0)) { return; }

        //鼠标左右移动
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        //鼠标上下移动
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX = _rotationX - Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

            float rotationY = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
        }
        //鼠标同时左右、上下移动
        else
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
        }
    }
}
