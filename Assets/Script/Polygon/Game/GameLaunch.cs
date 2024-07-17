using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class GameLaunch :SingletonMono<GameLaunch>
    {

        protected override void Awake()
        {
            base.Awake();


            #region 模块初始化
            //资源加载
            this.gameObject.AddComponent<ABMgr>();
            this.gameObject.AddComponent<ResManager>();
            this.gameObject.AddComponent<EventManager>();

            #endregion


            this.gameObject.AddComponent<GameControl>();


        }


        void Start()
        {
            //检查资源更新

            //StartGame
            GameControl.Instance.StartGame();
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {

        }
    }
}
