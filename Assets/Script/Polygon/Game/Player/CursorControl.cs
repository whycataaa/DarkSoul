using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class CursorControl : MonoBehaviour
    {
        void Start()
        {
            //鼠标固定在屏幕中心
            Cursor.lockState=CursorLockMode.Locked;
        }
        void Update()
        {
            switch (PlayerInputHandler.Instance.IsShowCursor)
            {
                case true:
                    Cursor.lockState = CursorLockMode.None;
                    break;
                case false:
                    Cursor.lockState = CursorLockMode.Locked;
                    break;
            }
        }
    }
}
