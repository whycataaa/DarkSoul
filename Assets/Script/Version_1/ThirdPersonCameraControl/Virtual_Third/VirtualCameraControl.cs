using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class VirtualCameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera playerVirtualCamera;

    public Transform playerCameraTrans;
    public Transform enemyCameraTrans;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerVirtualCamera=GetComponent<CinemachineVirtualCamera>();
    }


}
