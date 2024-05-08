using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2CameraSwitch : Singleton<Scene2CameraSwitch>
{
    public CinemachineVirtualCamera playerCamera;
    public CinemachineVirtualCamera gamecamera;

    public ItemDrag itemDrag;
    protected override void Awake()
    {
        base.Awake();
                gamecamera.enabled = false;
        itemDrag.enabled = false;
    }
    private void Start()
    {
      playerCamera=GameObject.Find("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            itemDrag.enabled = true;
            playerCamera.enabled = false;
            gamecamera.enabled = true;
        }
    }


    public void SwitchTOPlayerCamera()
    {
        playerCamera.enabled = true;
        gamecamera.enabled = false;
    }
}
