using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    private Image hpBar;
    private StarterAssets.ThirdPersonControllerCopy player;
    // Start is called before the first frame update

    /*
        void Start()
        {
            base.Start();
            hpBar = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
            player = GameObject.Find("PlayerArmature").GetComponent<StarterAssets.ThirdPersonControllerCopy>();

        }*/

    public void UpdateHPBar()
    {
        if (hpBar == null)
        {
            hpBar = this.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>();
        }

        if (player == null)
        {
            player = GameObject.Find("PlayerArmature").GetComponent<StarterAssets.ThirdPersonControllerCopy>();
        }

        if (hpBar != null && player != null)
        {
            hpBar.fillAmount = (float)player.HP / player.MaxHP;
        }
    }
}
