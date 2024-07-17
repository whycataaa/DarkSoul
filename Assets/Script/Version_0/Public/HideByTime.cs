using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 计时，时间到隐藏
/// </summary>
public class HideByTime : MonoBehaviour
{
    void Start()
    {
        Invoke("Hide", 3);
    }

    void Hide()
    {
        gameObject.SetActive(false);
    }
}