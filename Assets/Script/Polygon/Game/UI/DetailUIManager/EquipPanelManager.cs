using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class EquipPanelManager : MonoBehaviour
    {

        public EquipPanel equipPanel;
        public WeaponManager weaponManager;
        void Start()
        {
            equipPanel=new EquipPanel();
            equipPanel.weaponManager=weaponManager;
            PanelManager.Instance.PanelPush(equipPanel);
            equipPanel.Init();
        }


        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            if(PlayerInputHandler.Instance.IsSwitchLWeapon)
            {
                equipPanel.SwitchLWeapon();
            }

            if(PlayerInputHandler.Instance.IsSwitchRWeapon)
            {
                equipPanel.SwitchRWeapon();
            }
        }
    }
}
