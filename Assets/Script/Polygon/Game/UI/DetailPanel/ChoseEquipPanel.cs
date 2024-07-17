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
        Button buttonN;

        public ChoseEquipPanel() : base(new UIType(path))
        {

        }


        public override void Init()
        {
            buttonL=UITool.Instance.GetORAddComponentInChildren<Button>("Button_L");
            buttonR=UITool.Instance.GetORAddComponentInChildren<Button>("Button_R");
            buttonT=UITool.Instance.GetORAddComponentInChildren<Button>("Button_T");
            buttonN=UITool.Instance.GetORAddComponentInChildren<Button>("Button_N");

            buttonL.onClick.AddListener(()=>LEquip());
            buttonR.onClick.AddListener(()=>REquip());
            buttonT.onClick.AddListener(()=>TEquip());
            buttonN.onClick.AddListener(()=>NEquip());
        }


        /// <summary>
        /// 左手装备
        /// </summary>
        void LEquip()
        {

            EventTriggerExt.TriggerEvent(this,EventName.EquipWeapon,new ItemEventArgs{BagItemID=choseItemID,handState=HandState.LeftHand});

            choseItemID=-1;
            PanelManager.Instance.PanelPop();
        }

        /// <summary>
        /// 右手装备
        /// </summary>
        void REquip()
        {

            EventTriggerExt.TriggerEvent(this,EventName.EquipWeapon,new ItemEventArgs{BagItemID=choseItemID,handState=HandState.RightHand});
            choseItemID=-1;
            PanelManager.Instance.PanelPop();
        }

        /// <summary>
        /// 双手装备
        /// </summary>
        void TEquip()
        {
            EventTriggerExt.TriggerEvent(this,EventName.EquipWeapon,new ItemEventArgs{BagItemID=choseItemID,handState=HandState.TwoHands});
            choseItemID=-1;
            PanelManager.Instance.PanelPop();
        }

        /// <summary>
        /// 卸下装备
        /// </summary>
        void NEquip()
        {
            EventTriggerExt.TriggerEvent(this,EventName.EquipWeapon,new ItemEventArgs{BagItemID=choseItemID,handState=HandState.None});
            choseItemID=-1;
            PanelManager.Instance.PanelPop();
        }
        
    }
}
