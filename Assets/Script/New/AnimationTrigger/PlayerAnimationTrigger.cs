using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTrigger : MonoBehaviour
{
    public PlayerControl playerController;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerController=GetComponentInParent<PlayerControl>();
    }

    public void CanDamage()
    {
        playerController.CanDamage=true;
    }
    public void CannotDamage()
    {
        playerController.CanDamage=false;
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
