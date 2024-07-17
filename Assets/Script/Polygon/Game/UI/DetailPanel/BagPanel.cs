using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace PolygonProject
{
    public class BagPanel : BasePanel
    {
        BagPanelManager bagPanelManager;
        int MaxSlotNum=10;
        List<RectTransform> slotTrans=new List<RectTransform>();
        static readonly string path="AssetPackage/GUI/Panel_Bag";
        //根据格子的ID对应格子，方便对格子进行改动
        public Dictionary<int,Slot> slotDic=new Dictionary<int, Slot>();

        public BagPanel(BagPanelManager _bagPanelManager):base(new UIType(path))
        {
            bagPanelManager=_bagPanelManager;
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnPause()
        {

        }

        /// <summary>
        /// 初始化背包
        /// </summary>
        public override void Init()
        {

            //拿到物品数据并显示到UI上
            var bagItems=bagPanelManager.GetBagItems();
            //放格子
            for(int i=0;i<MaxSlotNum;i++)
            {
                //格子背景
                var go=GameObject.Instantiate<GameObject>(
                                                          ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                          UITool.Instance.GetORAddComponentInChildren<RectTransform>("EquipSlotTrans"));
                go.name="SlotBG"+i;
                slotTrans.Add(go.GetComponent<RectTransform>());
                //格子里的物品
                var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                   go.transform);
                item.name="Slot"+i;
                //加入脚本，可以在鼠标进入时显示信息
                var slot=item.AddComponent<Slot>();
                slotDic.Add(i,slot);
            }
            //将每个物品放入格子
            foreach(var bagItem in bagItems)
            {
                if(bagItems.Count>MaxSlotNum)
                {
                    Debug.Log("背包格子不够");
                }
                for(int i=0;i<MaxSlotNum;i++)
                {
                    if(slotDic[i].BagItemID==-1)
                    {
                        slotDic[i].BagItemID=bagItem.item.id;
                        slotDic[i].ShowSlot(true);
                        break;
                    }
                }
            }
            //多余的格子关闭
            for(int i=0;i<MaxSlotNum;i++)
            {
                if(slotDic[i].BagItemID==-1)
                slotDic[i].ShowSlot(false);
            }

        }

        /// <summary>
        /// UI上增加某个物品
        /// </summary>
        /// <param name="_itemID"></param>
        public void AddItem(int _itemID)
        {
            for(int i=0;i<MaxSlotNum;i++)
            {
                if(slotDic[i].BagItemID==_itemID)
                {
                    slotDic[i].Num++;
                    break;
                }
                if(slotDic[i].BagItemID==-1)
                {
                    slotDic[i].BagItemID=_itemID;
                    slotDic[i].Num=1;
                    slotDic[i].ShowSlot(true);
                }
            }
        }

        /// <summary>
        /// UI上移除某个物品
        /// </summary>
        /// <param name="_itemID"></param>
        public void RemoveItem(int _itemID)
        {
            for(int i=0;i<slotDic.Count;i++)
            {
                if(slotDic[i].BagItemID==_itemID)
                {
                    slotDic[i].Num--;
                    if(slotDic[i].Num==0)
                    {
                        slotDic[i].BagItemID=-1;
                        slotDic[i].ShowSlot(false);
                    }
                }
            }
        }
    }
}
