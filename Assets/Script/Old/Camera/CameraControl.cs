using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 控制地图中的所有相机
/// </summary>
public class CameraControl : Singleton<CameraControl>
{
    [SerializeField] private GameObject camerMain;
    [SerializeField] private GameObject camerMap;
    [SerializeField] private GameObject camer2D;//做2d游戏的相机

    public GameObject main;
    public Button closeBoook_Bt;
    public CinemachineVirtualCamera V_cameraMain;
   // public CinemachineVirtualCamera V_cameraBook;
    public CinemachineVirtualCamera V_cameraGame;
    public CinemachineVirtualCamera V_cameraTalk;

    protected override void Awake()
    {
        base.Awake();
        camer2D.SetActive(false);
        //   V_cameraBook.enabled = false;
        V_cameraTalk.enabled = false;
        V_cameraGame.enabled = false;
    }
    public void Start2DScene()
    {
        closeBoook_Bt.gameObject.SetActive(true);
        main.SetActive(false);
        camerMain.SetActive(false);
        camerMap.SetActive(false);
        camer2D.SetActive(true);


   }
    public void END2DScene()
    {
        main.SetActive(true);
        camerMain.SetActive(true);
        camerMap.SetActive(true);
        camer2D.SetActive(false);


    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) { Instance.Start2DScene(); }
    }

    public void StartTalk()
    {
        V_cameraMain.enabled = false;
        V_cameraTalk.enabled=true;
    }
    public void EndTalk()
    {
        V_cameraMain.enabled = true;
        V_cameraTalk.enabled = false;
    }

}
