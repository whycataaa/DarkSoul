using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace PolygonProject
{
    public class EquipPanelManager : MonoBehaviour
    {
        //UI
        public EquipPanel equipPanel;
        //数据以及游戏中武器的显示
        public WeaponManager weaponManager;


        void Start()
        {
            equipPanel=new EquipPanel(this);
            PanelManager.Instance.PanelPush(equipPanel);
            equipPanel.Init();

            EventManager.Instance.AddListener(EventName.EquipWeapon,EquipWeapon);
            EventManager.Instance.AddListener(EventName.UnEquipWeapon,UnEquipWeapon);
        }




        private void EquipWeapon(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            weaponManager.AddWeapon(data.BagItemID,data.handState);
            weaponManager.RefreshWeapon(true);
            weaponManager.RefreshWeapon(false);
            equipPanel.RefreshUI();
            weaponManager.ReSetAttackTimes();
        }
        private void UnEquipWeapon(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            weaponManager.RemoveWeapon(data.BagItemID);
            weaponManager.RefreshWeapon(true);
            weaponManager.RefreshWeapon(false);
            equipPanel.RefreshUI();
            weaponManager.ReSetAttackTimes();
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
        }

        //先刷新索引
        //更新武器实体
        //更新UI
        private void SwitchLWeapon()
        {
            weaponManager.ReSetAttackTimes();
            weaponManager.SwitchLWeapon();
            weaponManager.RefreshWeapon(true);
            equipPanel.RefreshUI();
        }
        private void SwitchRWeapon()
        {
            weaponManager.ReSetAttackTimes();
            weaponManager.SwitchRWeapon();
            weaponManager.RefreshWeapon(false);
            equipPanel.RefreshUI();
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
