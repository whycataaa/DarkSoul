using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using System;
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

        public Image[] slotsL;
        public Image[] slotsR;
        public Image[] slotsT;
        public Image[] slotsD;

        public Image Image_Head;
        public Image Image_Hand;
        public Image Image_Body;
        public Image Image_Leg;
        public Image Image_Feet;
        public TextMeshProUGUI[] Text_L;
        public TextMeshProUGUI[] Text_R;
        public TextMeshProUGUI[] Text_T;
        public TextMeshProUGUI[] Text_D;
        //UI
        RectTransform LBG;
        RectTransform RBG;
        RectTransform TBG;
        RectTransform DBG;
        RectTransform ArmorTrans;
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
            //物品栏//
            #region 初始化物品栏
            slotsL=new Image[bagPanelManager.GetBagData().DefaultLCount];
            slotsR=new Image[bagPanelManager.GetBagData().DefaultRCount];
            slotsD=new Image[bagPanelManager.GetBagData().DefaultDCount];
            slotsT=new Image[bagPanelManager.GetBagData().DefaultTCount];

            Text_L=new TextMeshProUGUI[bagPanelManager.GetBagData().DefaultLCount];
            Text_R=new TextMeshProUGUI[bagPanelManager.GetBagData().DefaultRCount];
            Text_D=new TextMeshProUGUI[bagPanelManager.GetBagData().DefaultDCount];
            Text_T=new TextMeshProUGUI[bagPanelManager.GetBagData().DefaultTCount];

            LBG=UITool.Instance.GetORAddComponentInChildren<RectTransform>("Image_BGL");
            RBG=UITool.Instance.GetORAddComponentInChildren<RectTransform>("Image_BGR");
            TBG=UITool.Instance.GetORAddComponentInChildren<RectTransform>("Image_BGT");
            DBG=UITool.Instance.GetORAddComponentInChildren<RectTransform>("Image_BGD");
            ArmorTrans=UITool.Instance.GetORAddComponentInChildren<RectTransform>("Image_BG_Armor");
            #endregion

            #region 初始化格子背景
            for(int i=0;i<slotsL.Length;i++)
            {
                var item=InitSlot("SlotBGL"+i,"SlotL"+i,LBG);
                slotsL[i]=item.GetComponent<Image>();
                Text_L[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsR.Length;i++)
            {
                var item=InitSlot("SlotBGR"+i,"SlotR"+i,RBG);
                slotsR[i]=item.GetComponent<Image>();
                Text_R[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsD.Length;i++)
            {
                var item=InitSlot("SlotBGD"+i,"SlotD"+i,DBG);
                slotsD[i]=item.GetComponent<Image>();
                Text_D[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsT.Length;i++)
            {
                var item=InitSlot("SlotBGT"+i,"SlotT"+i,TBG);
                slotsT[i]=item.GetComponent<Image>();
                Text_T[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }

            var head=InitSlot("Slot_ArmorBG_Head","Slot_Armor_Head",ArmorTrans);
            var hand=InitSlot("Slot_ArmorBG_Hand","Slot_Armor_Hand",ArmorTrans);
            var body=InitSlot("Slot_ArmorBG_Body","Slot_Armor_Body",ArmorTrans);
            var leg=InitSlot("Slot_ArmorBG_Leg","Slot_Armor_Leg",ArmorTrans);
            var feet=InitSlot("Slot_ArmorBG_Feet","Slot_Armor_Feet",ArmorTrans);
            Image_Head=head.GetComponent<Image>();
            Image_Hand=hand.GetComponent<Image>();
            Image_Body=body.GetComponent<Image>();
            Image_Leg=leg.GetComponent<Image>();
            Image_Feet=feet.GetComponent<Image>();
            #endregion


            //背包//
            //拿到物品数据并显示到UI上
            var bagItemDic=bagPanelManager.GetBagItemDic();
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

            //将每个物品放入背包格子
            foreach(var bagItem in bagItemDic)
            {
                for(int i=0;i<MaxSlotNum;i++)
                {
                    if(slotDic[i].BagItemID==-1)
                    {
                        slotDic[i].BagItemID=bagItem.Key;
                        slotDic[i].Num=bagItem.Value.Num;

                        //根据状态决定是否装备
                        switch(bagItem.Value.ItemEquipState)
                        {
                            case EItemEquipState.Equipped:
                                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=bagItem.Key});
                                break;
                            case EItemEquipState.TwoHandEquipped:
                                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=bagItem.Key,EDerection=EDerection.Left});
                                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=bagItem.Key,EDerection=EDerection.Right});
                                break;
                            case EItemEquipState.LeftHandEquipped:
                                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=bagItem.Key,EDerection=EDerection.Left});
                                break;
                            case EItemEquipState.RightHandEquipped:
                                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=bagItem.Key,EDerection=EDerection.Right});
                                break;
                        }
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

        private GameObject InitSlot(string _SlotBGName,string _ImageName,RectTransform _Trans)
        {
            var go=GameObject.Instantiate<GameObject>(
                                                        ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                        _Trans);
            go.name=_SlotBGName;
            var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                        go.transform);
            item.name=_ImageName;
            return item;
        }

        /// <summary>
        /// UI上增加某个物品
        /// </summary>
        /// <param name="_itemID"></param>
        public void AddItem(int _itemID)
        {
            Debug.Log("增加物品");
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
                    break;
                }
            }
        }
        public void AddItem(int _itemID,int _Num)
        {
            Debug.Log("增加物品");
            for(int i=0;i<MaxSlotNum;i++)
            {
                if(slotDic[i].BagItemID==_itemID)
                {
                    slotDic[i].Num+=_Num;
                    break;
                }
                if(slotDic[i].BagItemID==-1)
                {
                    slotDic[i].BagItemID=_itemID;
                    slotDic[i].Num+=_Num;
                    slotDic[i].ShowSlot(true);
                    break;
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

        public void RefreshInventoryUI()
        {
            for(int i=0;i<slotsL.Length;i++)
            {
                if(bagPanelManager.GetBagData().GetEquippedItems(EDerection.Left)[i]!=-1)
                {
                    slotsL[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Left)[i]].item.GetSprite();
                    Text_L[i].text=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Left)[i]].Num.ToString();
                    slotsL[i].gameObject.SetActive(true);
                }
                else
                {
                    slotsL[i].gameObject.SetActive(false);
                }
            }
            for(int i=0;i<slotsR.Length;i++)
            {
                if(bagPanelManager.GetBagData().GetEquippedItems(EDerection.Right)[i]!=-1)
                {
                    slotsR[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Right)[i]].item.GetSprite();
                    Text_R[i].text=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Right)[i]].Num.ToString();
                    slotsR[i].gameObject.SetActive(true);
                }
                else
                {
                    slotsR[i].gameObject.SetActive(false);
                }
            }
            for(int i=0;i<slotsT.Length;i++)
            {
                if(bagPanelManager.GetBagData().GetEquippedItems(EDerection.Up)[i]!=-1)
                {
                    slotsT[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Up)[i]].item.GetSprite();
                    Text_T[i].text=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Up)[i]].Num.ToString();
                    slotsT[i].gameObject.SetActive(true);
                }
                else
                {
                    slotsT[i].gameObject.SetActive(false);
                }
            }
            for(int i=0;i<slotsD.Length;i++)
            {
                if(bagPanelManager.GetBagData().GetEquippedItems(EDerection.Down)[i]!=-1)
                {
                    slotsD[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Down)[i]].item.GetSprite();
                    Text_D[i].text=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Down)[i]].Num.ToString();
                    slotsD[i].gameObject.SetActive(true);
                }
                else
                {
                    slotsD[i].gameObject.SetActive(false);
                }
            }
            if(bagPanelManager.GetBagData().ArmorHead!=-1)
            {
                Image_Head.sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().ArmorHead].item.GetSprite();
                Image_Head.gameObject.SetActive(true);
            }
            else
            {
                Image_Head.sprite=null;
                Image_Head.gameObject.SetActive(false);
            }
            if(bagPanelManager.GetBagData().ArmorFeet!=-1)
            {
                Image_Feet.sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().ArmorFeet].item.GetSprite();
                Image_Feet.gameObject.SetActive(true);
            }
            else
            {
                Image_Feet.sprite=null;
               Image_Feet.gameObject.SetActive(false);
            }
            if(bagPanelManager.GetBagData().ArmorBody!=-1)
            {
                Image_Body.sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().ArmorBody].item.GetSprite();
                Image_Body.gameObject.SetActive(true);
            }
            else
            {
                Image_Body.sprite=null;
                Image_Body.gameObject.SetActive(false);
            }
            if(bagPanelManager.GetBagData().ArmorLeg!=-1)
            {
                Image_Leg.sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().ArmorLeg].item.GetSprite();
                Image_Leg.gameObject.SetActive(true);
            }
            else
            {
                Image_Leg.sprite=null;
                Image_Leg.gameObject.SetActive(false);
            }
            if(bagPanelManager.GetBagData().ArmorHand!=-1)
            {
                Image_Hand.sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().ArmorHand].item.GetSprite();
                Image_Hand.gameObject.SetActive(true);
            }
            else
            {
                Image_Hand.sprite=null;
                Image_Hand.gameObject.SetActive(false);
            }

        }


    }
}
