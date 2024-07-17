using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    /// <summary>
    /// 玩家初始装备面板
    /// </summary>
    public class EquipPanel : BasePanel
    {

        public WeaponManager weaponManager;
        static readonly string path="AssetPackage/GUI/Panel_Equip";

        public EquipPanel():base(new UIType(path))
        {

        }

        /// <summary>
        /// 当前装备物品的UI图片
        /// </summary>
        Image image_L;
        Image image_R;
        Image image_T;
        Image image_D;


        public override void Init()
        {
            image_L=UITool.Instance.GetORAddComponentInChildren<Image>("Image_L");
            image_R=UITool.Instance.GetORAddComponentInChildren<Image>("Image_R");
            image_T=UITool.Instance.GetORAddComponentInChildren<Image>("Image_T");
            image_D=UITool.Instance.GetORAddComponentInChildren<Image>("Image_D");

            if(weaponManager.weaponsL.Count>0)
            {
                image_L.sprite=weaponManager.GetWeaponDic()[weaponManager.weaponsL[0]].sprite;
            }
            if(weaponManager.weaponsR.Count>0)
            {
                image_R.sprite=weaponManager.GetWeaponDic()[weaponManager.weaponsR[0]].sprite;
            }


        }


        public void SwitchLWeapon()
        {
            //攻击次数重置
            weaponManager.AttackTimes=1;
            if(weaponManager.weaponsL.Count>0)
            {
                //索引加1
                weaponManager.currentWeaponIndexL++;
                if(weaponManager.currentWeaponIndexL>=weaponManager.weaponsL.Count)
                {
                    weaponManager.currentWeaponIndexL=0;
                }

                weaponManager.EquipWeapon(weaponManager.currentWeaponIndexL,true);


                image_L.sprite=weaponManager.GetWeaponDic()[weaponManager.weaponsL[weaponManager.currentWeaponIndexL]].sprite;
                weaponManager.SetCurrentWeapon(weaponManager.currentWeaponIndexL,true);
            }
        }

        public void SwitchRWeapon()
        {
            weaponManager.AttackTimes=1;
            if(weaponManager.weaponsR.Count>0)
            {
                //索引加1
                weaponManager.currentWeaponIndexR++;
                if(weaponManager.currentWeaponIndexR>=weaponManager.weaponsR.Count)
                {
                    weaponManager.currentWeaponIndexR=0;
                }

                weaponManager.EquipWeapon(weaponManager.currentWeaponIndexR,false);

                image_R.sprite=weaponManager.GetWeaponDic()[weaponManager.weaponsR[weaponManager.currentWeaponIndexR]].sprite;
                weaponManager.SetCurrentWeapon(weaponManager.currentWeaponIndexR,false);
            }

        }

    }
}
