using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
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
        public TextMeshProUGUI[] Text_L;
        public TextMeshProUGUI[] Text_R;
        public TextMeshProUGUI[] Text_T;
        public TextMeshProUGUI[] Text_D;
        //UI
        RectTransform LBG;
        RectTransform RBG;
        RectTransform TBG;
        RectTransform DBG;
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
            //初始化物品栏
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

            #region 初始化格子背景
            for(int i=0;i<slotsL.Length;i++)
            {
                var go=GameObject.Instantiate<GameObject>(
                                                          ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                          LBG);
                go.name="SlotBGL"+i;
                var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                   go.transform);
                item.name="SlotL"+i;
                slotsL[i]=item.GetComponent<Image>();
                Text_L[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsR.Length;i++)
            {
                var go=GameObject.Instantiate<GameObject>(
                                                          ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                          RBG);
                go.name="SlotBGR"+i;
                var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                   go.transform);
                item.name="SlotR"+i;
                slotsR[i]=item.GetComponent<Image>();
                Text_R[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsD.Length;i++)
            {
                var go=GameObject.Instantiate<GameObject>(
                                                          ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                          DBG);
                go.name="SlotBGD"+i;
                var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                   go.transform);
                item.name="SlotD"+i;
                slotsD[i]=item.GetComponent<Image>();
                Text_D[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            for(int i=0;i<slotsT.Length;i++)
            {
                var go=GameObject.Instantiate<GameObject>(
                                                          ResManager.Instance.LoadResource<GameObject>("GUI","Image_Slot.prefab"),
                                                          TBG);
                go.name="SlotBGT"+i;
                var item=GameObject.Instantiate<GameObject>(ResManager.Instance.LoadResource<GameObject>("GUI","Image_Item.prefab"),
                                                   go.transform);
                item.name="SlotT"+i;
                slotsT[i]=item.GetComponent<Image>();
                Text_T[i]=item.GetComponentInChildren<TextMeshProUGUI>();
            }
            #endregion



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
            //将每个物品放入背包格子
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

        public void RefreshInventoryUI()
        {
            for(int i=0;i<slotsL.Length;i++)
            {
                if(bagPanelManager.GetBagData().GetEquippedItems(EDerection.Left)[i]!=-1)
                {
                    slotsL[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Left)[i]].item.sprite;
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
                    slotsR[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Right)[i]].item.sprite;
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
                    slotsT[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Up)[i]].item.sprite;
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
                    slotsD[i].sprite=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Down)[i]].item.sprite;
                    Text_D[i].text=bagPanelManager.GetBagItemDic()[bagPanelManager.GetBagData().GetEquippedItems(EDerection.Down)[i]].Num.ToString();
                    slotsD[i].gameObject.SetActive(true);
                }
                else
                {
                    slotsD[i].gameObject.SetActive(false);
                }
            }
        }


    }
}
