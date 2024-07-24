using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace PolygonProject
{
    public class GameControl : SingletonMono<GameControl>
    {
        protected override void Awake()
        {
            base.Awake();
        }
        public void StartGame()
        {
            EnterGameScene();
        }



        private void EnterGameScene()
        {
            #region 数据
            //加载数据曲线
            GameObject DataPrefab=ResManager.Instance.LoadResource<GameObject>("Data","Data.prefab");
            GameObject Data=Instantiate(DataPrefab);
            Data.name="Data";

            var bagData=new BagData();
            bagData.Init();
            DataBoard.Instance.Init();
            DataBoard.Instance.BagData=bagData;
            #endregion
            #region 地图
            GameObject mapPrefab=ResManager.Instance.LoadResource<GameObject>("Maps","MapTest.prefab");
            GameObject map=Instantiate(mapPrefab);
            map.name="Map1";
            #endregion

            #region 相机
            GameObject playerCameraPrefab=ResManager.Instance.LoadResource<GameObject>("Cameras","PlayerCamera.prefab");
            GameObject mainCameraPrefab=ResManager.Instance.LoadResource<GameObject>("Cameras","MainCamera.prefab");
            GameObject lockCameraPrefab=ResManager.Instance.LoadResource<GameObject>("Cameras","PlayerCameraLock.prefab");
            GameObject playerCamera=Instantiate(playerCameraPrefab);
            GameObject mainCamera=Instantiate(mainCameraPrefab);
            GameObject lockCamera=Instantiate(lockCameraPrefab);
            playerCamera.name=playerCameraPrefab.name;
            mainCamera.name=mainCameraPrefab.name;
            lockCamera.name=lockCameraPrefab.name;
            //相机缩放
            playerCamera.AddComponent<CameraZoom>();
            playerCamera.AddComponent<CameraControl>();
            this.AddComponent<CursorControl>();
            #endregion

            #region 角色

            GameObject playerPrefab=ResManager.Instance.LoadResource<GameObject>("Characters","Player.prefab");
            GameObject player=Instantiate(playerPrefab);
            player.name="Player";

            GameObject enemyTestPrefab=ResManager.Instance.LoadResource<GameObject>("Characters","Skeleton.prefab");
            GameObject enemyTest=Instantiate(enemyTestPrefab);
            enemyTest.name="Skeleton";
            var playerControl=player.AddComponent<PlayerControl>();


            #endregion

            #region UI
            this.AddComponent<PlayerBasePanelManager>();
            var equipPanelManager=this.AddComponent<EquipPanelManager>();
            var bagPanelManager=this.AddComponent<BagPanelManager>();

            bagPanelManager.SetBagData(bagData);

            this.AddComponent<ShowItemPanelManager>();
            this.AddComponent<ChosePanelManager>();
            #endregion

            #region 初始化
            //相机
            var playerCameraRoot=TransformHelper.FindDeepTransform<Transform>(player.transform,"CameraRoot");

            playerCamera.GetComponent<CameraControl>().SetCameraAim(playerCameraRoot,playerCamera);
            playerCamera.GetComponent<CameraControl>().SetCameraFollow(playerCameraRoot,lockCamera);
            playerCamera.GetComponent<CameraControl>().SetCameraFollow(playerCameraRoot,playerCamera);



            WeaponManager weaponManager=new WeaponManager();
            weaponManager.player=player;
            weaponManager.bagData=bagPanelManager.GetBagData();
            weaponManager.Init();

            playerControl.playerWeaponManager=weaponManager;
            equipPanelManager.weaponManager=weaponManager;
            equipPanelManager.bagPanelManager=bagPanelManager;
            // PlayerData playerData=new PlayerData();
            // playerData.Load();



            #endregion
        }

        /// <summary>
        /// Start is called on the frame when a script is enabled just before
        /// any of the Update methods is called the first time.
        /// </summary>
        void Start()
        {

        }
    }
}
