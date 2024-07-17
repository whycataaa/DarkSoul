using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Player_Base:MonoBehaviour
{



    [SerializeField]private Image playerHealthBar;
    [SerializeField]private Image playerHealthEffect;
    [SerializeField]private Image playerStaminaBar;
    [SerializeField]private Image playerStaminaEffect;
    //缓冲时间
    private float bufferTime = 0.5f;
    private Coroutine coroutine1;
    private Coroutine coroutine2;
    public void FillPlayerHPBar(float value,float maxValue)
    {
        playerHealthBar.fillAmount=value/maxValue;
        if(coroutine1!=null)
        {
            StopCoroutine(coroutine1);
        }
        coroutine1=StartCoroutine(UpdateEffect(playerHealthBar,playerHealthEffect));
    }
    public void FillPlayerStaminaBar(float value,float maxValue)
    {
        playerStaminaBar.fillAmount=value/maxValue;
        if(coroutine2!=null)
        {
            StopCoroutine(coroutine2);
        }
        coroutine2=StartCoroutine(UpdateEffect(playerStaminaBar,playerStaminaEffect));
    }

    /// <summary>
    /// 输入两个条的图像，进行缓冲效果
    /// </summary>
    /// <param name="bar">状态条</param>
    /// <param name="effect">状态缓冲条</param>
    /// <returns></returns>
    private IEnumerator UpdateEffect(Image bar,Image effect)
    {
        float effectLength=effect.fillAmount-bar.fillAmount;
        float elapsedTime=0f;
        while(elapsedTime<bufferTime)
        {
            elapsedTime += Time.deltaTime;
            effect.fillAmount = Mathf.Lerp(bar.fillAmount + effectLength, bar.fillAmount, elapsedTime / bufferTime);
            yield return null;
        }

        effect.fillAmount = bar.fillAmount;
    }
}
