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
        EquipPanelManager equipPanelManager;
        static readonly string path="AssetPackage/GUI/Panel_Equip";

        public EquipPanel(EquipPanelManager _equipPanelManager):base(new UIType(path))
        {
            equipPanelManager=_equipPanelManager;
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
        }

        /// <summary>
        /// 刷新主装备面板UI
        /// </summary>
        /// <param name="_EDerection"></param>
        public void RefreshUI(EDerection _EDerection)
        {
            switch(_EDerection)
            {
                case EDerection.Left:
                    if(equipPanelManager.GetWeapons(true)[equipPanelManager
                                                    .GetCurrentWeaponIndex(true)]==-1)
                    {
                        image_L.gameObject.SetActive(false);
                    }
                    else
                    {
                        image_L.sprite=equipPanelManager.GetWeaponDic()[equipPanelManager
                                                    .GetWeapons(true)[equipPanelManager
                                                    .GetCurrentWeaponIndex(true)]]
                                                    .sprite;
                        image_L.gameObject.SetActive(true);
                    }
                    break;

                case EDerection.Right:
                    if(equipPanelManager.GetWeapons(false)[equipPanelManager
                                                    .GetCurrentWeaponIndex(false)]==-1)
                    {
                        image_R.gameObject.SetActive(false);
                    }
                    else
                    {
                        image_R.sprite=equipPanelManager.GetWeaponDic()[equipPanelManager
                                                        .GetWeapons(false)[equipPanelManager
                                                        .GetCurrentWeaponIndex(false)]]
                                                        .sprite;
                        image_R.gameObject.SetActive(true);
                    }
                    break;

                case EDerection.Up:
                    if(DataBoard.Instance.BagData.GetEquippedItems(EDerection.Up)
                                                    [DataBoard.Instance.BagData.GetCurrentIndex(EDerection.Up)]==-1)
                    {
                        image_T.gameObject.SetActive(false);
                    }
                    else
                    {
                        image_T.sprite=DataBoard.Instance.BagData.GetItemDic()
                                                                [
                                                                    DataBoard.Instance.BagData.GetEquippedItems(EDerection.Up)
                                                                    [DataBoard.Instance.BagData.GetCurrentIndex(EDerection.Up)]
                                                                ]
                                                                .sprite;
                        image_T.gameObject.SetActive(true);
                    }

                    break;

                case EDerection.Down:
                    if(DataBoard.Instance.BagData.GetEquippedItems(EDerection.Down)
                                                    [DataBoard.Instance.BagData.GetCurrentIndex(EDerection.Down)]==-1)
                    {
                        image_D.gameObject.SetActive(false);
                    }
                    else
                    {
                        image_D.sprite=DataBoard.Instance.BagData.GetItemDic()
                                                                [
                                                                    DataBoard.Instance.BagData.GetEquippedItems(EDerection.Down)
                                                                    [DataBoard.Instance.BagData.GetCurrentIndex(EDerection.Down)]
                                                                ]
                                                                .sprite;
                        image_D.gameObject.SetActive(true);
                    }
                    break;

            }
        }

    }

}
