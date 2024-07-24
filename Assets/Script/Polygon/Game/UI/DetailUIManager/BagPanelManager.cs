using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{

    /// <summary>
    /// 背包的控制中心
    /// </summary>
    public class BagPanelManager : MonoBehaviour
    {
        BagPanel bagPanel;
        BagData bagData;
        bool IsOpenBag=false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            bagPanel=new BagPanel(this);
        }
        void Start()
        {
            PanelManager.Instance.PanelPush(bagPanel);
            //背包和装备界面数据初始化
            bagPanel.Init();
            PanelManager.Instance.PanelPop();
            //物品栏刷新
            bagPanel.RefreshInventoryUI();
            EventManager.Instance.AddListener(EventName.UseItem,RemoveItem);
            EventManager.Instance.AddListener(EventName.AddItem,AddItem);


        }


        void Update()
        {
            if(PlayerInputHandler.Instance.IsBag)
            {
                //打开背包
                if(!IsOpenBag)
                {
                    //Debug.Log("打开背包");
                    PanelManager.Instance.PanelPush(bagPanel);
                    IsOpenBag=true;
                }
                //关闭背包
                else
                {
                    while(PanelManager.Instance.GetPanelStack().Peek().UIType!=bagPanel.UIType)
                    {
                        PanelManager.Instance.PanelPop();
                    }
                    PanelManager.Instance.PanelPop();

                    IsOpenBag=false;
                }
            }
        }


        void OnDestroy()
        {
            EventManager.Instance.RemoveListener(EventName.AddItem,AddItem);
            EventManager.Instance.RemoveListener(EventName.UseItem,RemoveItem);
        }
        public void AddItem(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            Debug.Log(bagData==null);
            bagData.AddItem(data.BagItemID,data.ItemNum);
            bagPanel.AddItem(data.BagItemID,data.ItemNum);
        }

        public void RemoveItem(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            bagData.RemoveItem(data.BagItemID);
            bagPanel.RemoveItem(data.BagItemID);
        }

        /// <summary>
        /// 通过ID查找某个物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public BagItem FindItemByID(int _id)
        {
            return bagData.FindItemByID(_id);
        }

        /// <summary>
        /// 通过名字查找某个物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public BagItem FindItemByName(string _name)
        {
            return bagData.FindItemByName(_name);
        }


        public Dictionary<int,BagItem> GetBagItemDic()
        {
            return bagData.GetBagItemDic();
        }
        public Dictionary<int,Item> GetItemDic()
        {
            return bagData.GetItemDic();
        }
        public BagData GetBagData()
        {
            return bagData;
        }

        public void SetBagData(BagData _bagData)
        {
            bagData=_bagData;
        }
        public void RefreshInventoryUI()
        {
            bagPanel.RefreshInventoryUI();
        }

    }
}
