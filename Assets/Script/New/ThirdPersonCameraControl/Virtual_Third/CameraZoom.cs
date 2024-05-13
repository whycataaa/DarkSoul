using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

/// <summary>
/// 鼠标滚轮控制相机缩放
/// </summary>
public class CameraZoom : MonoBehaviour
{
    [Header("默认相机距离")]
    [SerializeField] [Range(0f,10f)]private float defaultDistance =1.5f;
    [Header("最大相机距离")]
    [SerializeField] [Range(0f,10f)]private float maxDistance =3f;
    [Header("最小相机距离")]
    [SerializeField] [Range(0f,10f)]private float minDistance =0.9f;

    [SerializeField] [Range(0f,10f)]private float smoothing=2f;
    [Header("缩放灵敏度")]
    [SerializeField] [Range(0f,10f)]private float zoomSensitivity=1f;


    //当前相机要到达的距离
    private float currentTargetDistance;
    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        framingTransposer=GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider=GetComponent<CinemachineInputProvider>();

        currentTargetDistance=defaultDistance;

    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Zoom();
    }

    /// <summary>
    /// 相机缩放
    /// </summary>
    private void Zoom()
    {
        float zoomValue=inputProvider.GetAxisValue(2)*zoomSensitivity;

        currentTargetDistance=Mathf.Clamp(currentTargetDistance+zoomValue,minDistance,maxDistance);

        float currentDistance=framingTransposer.m_CameraDistance;

        if(currentDistance==currentTargetDistance)
        {
            return;
        }
        //实现平滑过渡
        float lerpedZoomValue=Mathf.Lerp(currentDistance,currentTargetDistance,smoothing*Time.deltaTime);

        framingTransposer.m_CameraDistance=lerpedZoomValue;
    }
}
