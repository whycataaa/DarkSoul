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
            bagPanel.Init();
            PanelManager.Instance.PanelPop();

            EventManager.Instance.AddListener(EventName.UseItem,AddItem);
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
            EventManager.Instance.RemoveListener(EventName.UseItem, AddItem);
        }
        public void AddItem(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            bagData.AddItem(data.BagItemID);
            bagPanel.AddItem(data.BagItemID);
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

        public List<BagItem> GetBagItems()
        {
            return bagData.GetBagItems();
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
    }
}
