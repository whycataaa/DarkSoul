using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2
{
public class PlayerAnimationTrigger : MonoBehaviour
{
    public PlayerControl playerController;
    public WeaponManager weaponManager;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerController=GetComponentInParent<PlayerControl>();
        weaponManager=GetComponentInParent<WeaponManager>();
    }

    //开启检测
    public void CanDamage()
    {
        weaponManager.OnDetect=true;
    }
    //关闭检测
    public void CannotDamage()
    {
        weaponManager.OnDetect=false;
    }
    public void CanCombo()
    {
        playerController.CanCombo=true;
    }
    public void CannotCombo()
    {
        playerController.CanSwitch=true;
        playerController.CanCombo=false;

    }
    public void CannotSwitch()
    {
        playerController.CanSwitch=false;
    }


}
}