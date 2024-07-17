using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_Enemy_Base : MonoBehaviour
{

    public RectTransform rectTransform;
    public Dictionary<int,Hp> hpTable=new Dictionary<int, Hp>();
    #region boss血条
    [SerializeField]public GameObject panel_Enemy_BossBasePrefab;
    #endregion

    #region 小怪血条
    [SerializeField]public GameObject panel_Enemy_NormalBasePrefab;
    #endregion

    //缓冲时间
    private float bufferTime = 0.5f;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        rectTransform=GetComponent<RectTransform>();
    }
    /// <summary>
    /// 更新血条并缓冲
    /// </summary>
    /// <param name="value">更新后的值</param>
    /// <param name="maxValue">满状态值</param>
    public void FillEnemyHPBar(float value,float maxValue,int enemyID)
    {
        hpTable[enemyID].healthBar.fillAmount=value/maxValue;
        StartCoroutine(UpdateEffect(hpTable[enemyID].healthBar,hpTable[enemyID].healthEffect));
    }

    /// <summary>
    /// 是否显示boss血条
    /// </summary>
    /// <param name="value"></param>
    public void ShowEnemyHP(bool value)
    {
        panel_Enemy_BossBasePrefab.SetActive(value);
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
            effect.fillAmount = Mathf.Lerp(bar.fillAmount + effectLength, 
                                           bar.fillAmount, elapsedTime / bufferTime);
            yield return null;
        }

        effect.fillAmount = bar.fillAmount;
    }
}
    public struct Hp
    {
        public Image healthBar;
        public Image healthEffect;
    }