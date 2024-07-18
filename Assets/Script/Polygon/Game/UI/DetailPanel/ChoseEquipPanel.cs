using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    public class ChoseEquipPanel : BasePanel
    {
        static readonly string path="AssetPackage/GUI/Panel_Chose_Equip";

        public int choseItemID;
        Button buttonL;
        Button buttonR;
        Button buttonT;


        public ChoseEquipPanel() : base(new UIType(path))
        {

        }


        public override void Init()
        {
            buttonL=UITool.Instance.GetORAddComponentInChildren<Button>("Button_L");
            buttonR=UITool.Instance.GetORAddComponentInChildren<Button>("Button_R");
            buttonT=UITool.Instance.GetORAddComponentInChildren<Button>("Button_T");


            buttonL.onClick.AddListener(()=>LEquip());
            buttonR.onClick.AddListener(()=>REquip());
            buttonT.onClick.AddListener(()=>TEquip());

        }


        /// <summary>
        /// 左手装备
        /// </summary>
        void LEquip()
        {
            if(DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState!=ItemState.Equipped)
            {
                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=choseItemID,EDerection=EDerection.Left});
                DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState=ItemState.Equipped;
                choseItemID=-1;
                PanelManager.Instance.PanelPop();
            }
            else
            {
                Debug.Log("已经装备");
            }
        }

        /// <summary>
        /// 右手装备
        /// </summary>
        void REquip()
        {
            if(DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState!=ItemState.Equipped)
            {
                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=choseItemID,EDerection=EDerection.Right});
                DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState=ItemState.Equipped;
                choseItemID=-1;
                PanelManager.Instance.PanelPop();
            }
            else
            {
                Debug.Log("已经装备");
            }
        }

        /// <summary>
        /// 双手装备
        /// </summary>
        void TEquip()
        {
            if(DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState!=ItemState.Equipped)
            {
                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=choseItemID,EDerection=EDerection.Left});
                EventTriggerExt.TriggerEvent(this,EventName.EquipItem,new ItemEventArgs{BagItemID=choseItemID,EDerection=EDerection.Right});
                DataBoard.Instance.BagData.GetBagItemDic()[choseItemID].itemState=ItemState.Equipped;
                choseItemID=-1;
                PanelManager.Instance.PanelPop();
            }
            else
            {
                Debug.Log("已经装备");
            }
        }
    }
}
