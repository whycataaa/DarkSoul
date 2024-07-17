using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    //使用物品事件类
    public class ItemEventArgs:EventArgs
    {
        public int BagItemID;
        public HandState handState;
    }
    public class ChosePanel : BasePanel
    {
        public int ChoseItemID;
        #region 单例
        static ChosePanel instance;
        public static ChosePanel Instance
        {
            get
            {
                if(instance==null)
                {
                    instance=Activator.CreateInstance<ChosePanel>();
                }
                return instance;
            }
        }
        #endregion
        static readonly string path="AssetPackage/GUI/Panel_Chose";

        public ChosePanel():base(new UIType(path)){}

        Button button_Equip;
        Button button_Use;
        Button button_View;
        Button button_Throw;
        ChoseEquipPanel choseEquipPanel;
        public override void Init()
        {
            choseEquipPanel=new ChoseEquipPanel();
            PanelManager.Instance.PanelPush(choseEquipPanel);
            choseEquipPanel.Init();
            PanelManager.Instance.PanelPop();
            button_Equip=UITool.Instance.GetORAddComponentInChildren<Button>("Button1");
            button_Use=UITool.Instance.GetORAddComponentInChildren<Button>("Button2");
            button_View=UITool.Instance.GetORAddComponentInChildren<Button>("Button3");
            button_Throw=UITool.Instance.GetORAddComponentInChildren<Button>("Button4");

            button_Equip.onClick.AddListener(()=>Equip(ChoseItemID));
            button_Use.onClick.AddListener(()=>Use(ChoseItemID));
            button_View.onClick.AddListener(()=>View(ChoseItemID));
            button_Throw.onClick.AddListener(()=>Throw(ChoseItemID));
        }


        public override void OnEnter()
        {
            //根据选中物品的类别设置按钮状态
        }
        /// <summary>
        /// 点击装备按钮打开二级面板
        /// </summary>
        /// <param name="_bagItemID"></param>
        void Equip(int _bagItemID)
        {
            if(DataBoard.Instance.BagData.GetBagItemDic()[_bagItemID].item.itemType==ItemType.Weapon)
            {
                choseEquipPanel.choseItemID=_bagItemID;
                PanelManager.Instance.PanelPush(choseEquipPanel);

                UIManager.Instance.UIDic[choseEquipPanel.UIType].transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }

        }
        //使用物品方法
        void Use(int _bagItemID)
        {
            if(DataBoard.Instance.BagData.GetBagItemDic()[_bagItemID].item.itemType==ItemType.Useable)
            {
                EventTriggerExt.TriggerEvent(this,EventName.UseItem,new ItemEventArgs{BagItemID=_bagItemID});
            }

        }
        private void Throw(int _bagItemID)
        {
            
        }

        private void View(int _bagItemID)
        {
            
        }

    }
}
