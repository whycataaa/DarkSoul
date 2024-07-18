using System;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public enum EDerection
    {
        Left,
        Right,
        Up,
        Down
    }
    public class EquipPanelManager : MonoBehaviour
    {
        //UI
        public EquipPanel equipPanel;
        //数据以及游戏中武器的显示
        public WeaponManager weaponManager;
        public BagPanelManager bagPanelManager;
        void Start()
        {
            equipPanel=new EquipPanel(this);
            PanelManager.Instance.PanelPush(equipPanel);
            equipPanel.Init();
            equipPanel.RefreshUI(EDerection.Up);
            equipPanel.RefreshUI(EDerection.Down);
            equipPanel.RefreshUI(EDerection.Left);
            equipPanel.RefreshUI(EDerection.Right);
            EventManager.Instance.AddListener(EventName.EquipItem,EquipItem);
            EventManager.Instance.AddListener(EventName.UnEquipItem,UnEquipItem);
            EventManager.Instance.AddListener(EventName.UseItem,UseItem);
            EventManager.Instance.AddListener(EventName.ThrowItem,ThrowItem);
        }

        private void ThrowItem(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void UseItem(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EquipItem(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;

            switch(DataBoard.Instance.BagData.GetItemDic()[data.BagItemID].itemType)
            {
                case ItemType.Weapon:
                    weaponManager.AddWeapon(data.BagItemID,data.EDerection);
                    weaponManager.RefreshWeapon(true);
                    weaponManager.RefreshWeapon(false);
                    equipPanel.RefreshUI(EDerection.Left);
                    equipPanel.RefreshUI(EDerection.Right);
                    weaponManager.ReSetAttackTimes();
                    break;
                case ItemType.Spell:
                    DataBoard.Instance.BagData.AddEquipItem(data.BagItemID,EDerection.Up);
                    equipPanel.RefreshUI(EDerection.Up);
                    break;
                case ItemType.Useable:
                    DataBoard.Instance.BagData.AddEquipItem(data.BagItemID,EDerection.Down);
                    equipPanel.RefreshUI(EDerection.Down);
                    break;
                case ItemType.Collection:
                    Debug.Log("收集品不可装备");
                    break;
            }

            bagPanelManager.RefreshInventoryUI();
        }
        private void UnEquipItem(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            switch(DataBoard.Instance.BagData.GetItemDic()[data.BagItemID].itemType)
            {
                case ItemType.Weapon:
                    DataBoard.Instance.BagData.RemoveEquipItem(data.BagItemID,EDerection.Left);
                    DataBoard.Instance.BagData.RemoveEquipItem(data.BagItemID,EDerection.Right);
                    equipPanel.RefreshUI(EDerection.Left);
                    equipPanel.RefreshUI(EDerection.Right);
                    weaponManager.RefreshWeapon(true);
                    weaponManager.RefreshWeapon(false);
                    weaponManager.ReSetAttackTimes();
                    break;
                case ItemType.Useable:
                    DataBoard.Instance.BagData.RemoveEquipItem(data.BagItemID,EDerection.Down);
                    equipPanel.RefreshUI(EDerection.Down);
                    break;
                case ItemType.Spell:
                    DataBoard.Instance.BagData.RemoveEquipItem(data.BagItemID,EDerection.Up);
                    equipPanel.RefreshUI(EDerection.Up);
                    break;
            }

            bagPanelManager.RefreshInventoryUI();
        }



        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(PlayerInputHandler.Instance.IsSwitchLWeapon)
            {
                SwitchLWeapon();
            }

            if(PlayerInputHandler.Instance.IsSwitchRWeapon)
            {
                SwitchRWeapon();
            }
            if(PlayerInputHandler.Instance.IsSwitchUpItem)
            {
                SwitchUpItem();
            }
            if(PlayerInputHandler.Instance.IsSwitchDownItem)
            {
                SwitchDownItem();
            }
        }



        //先刷新索引
        //更新武器实体
        //更新UI
        private void SwitchLWeapon()
        {
            weaponManager.ReSetAttackTimes();
            weaponManager.SwitchLWeapon();
            weaponManager.RefreshWeapon(true);
            equipPanel.RefreshUI(EDerection.Left);
        }
        private void SwitchRWeapon()
        {
            weaponManager.ReSetAttackTimes();
            weaponManager.SwitchRWeapon();
            weaponManager.RefreshWeapon(false);
            equipPanel.RefreshUI(EDerection.Right);
        }
        private void SwitchUpItem()
        {
            DataBoard.Instance.BagData.SwitchUpItemData();
            equipPanel.RefreshUI(EDerection.Up);
        }
        private void SwitchDownItem()
        {
            DataBoard.Instance.BagData.SwitchDownItemData();
            equipPanel.RefreshUI(EDerection.Down);
        }


        #region PUBLIC
        /// <summary>
        /// 获取当前武器
        /// </summary>
        /// <param name="IsLeft"></param>
        /// <returns></returns>
        public Weapon GetCurrentWeapon(bool IsLeft)
        {
            return weaponManager.GetCurrentWeapon(IsLeft);
        }

        /// <summary>
        /// 获取当前武器的攻击次数
        /// </summary>
        /// <param name="IsLeft"></param>
        /// <returns></returns>
        public int GetCurrentWeaponMaxAttackTimes(bool IsLeft)
        {
            return weaponManager.GetCurrentWeaponMaxAttackTimes(IsLeft);
        }


        /// <summary>
        /// 获取当前武器字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,Weapon> GetWeaponDic()
        {
            return weaponManager.GetWeaponDic();
        }
        /// <summary>
        /// 获取当前武器槽的索引
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int GetCurrentWeaponIndex(bool IsLeft)
        {
            return weaponManager.GetCurrentWeaponIndex(IsLeft);
        }

        /// <summary>
        /// 获取最大可装备武器数量
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int GetMaxWeaponCount(bool IsLeft)
        {
            return weaponManager.GetMaxWeaponCount(IsLeft);
        }

        /// <summary>
        /// 获取当前武器栏位数组
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int[] GetWeapons(bool IsLeft)
        {
            return weaponManager.GetWeapons(IsLeft);
        }

        #endregion
    }
}
