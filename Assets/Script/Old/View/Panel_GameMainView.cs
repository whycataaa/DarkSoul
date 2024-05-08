using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 游戏主场景界面
/// </summary>
public class Panel_GameMainView : Singleton<Panel_GameMainView>
{
    //药水小物品图标：血量，体力，普通攻击力，魔法攻击力，普通防御力，魔法防御力
    [SerializeField] private Text item_HP, item_MP, item_Attack, item_Magic, item_ATKDefense, item_MGDefense, coin, taskName;
    //武器图标锁1，2，3
    //   [SerializeField] private GameObject weaponLock1, weaponLock2, weaponLock3;
    //血量条、体力条
    [SerializeField] private GameObject img_HP, img_MP;
    //任务名称
    [SerializeField] private string[] s_TaskName;
    //血量条宽度，血量条高度，当前血量值，当前体力值
    private float f_Panel_HPWidth, f_Panel_HPHight, f_Panel_MPWidth, f_Panel_MPHight;

    public GameObject panel_Exit;
    public Button bt1, bt2;


    protected override void Awake()
    {
        base.Awake();
        panel_Exit.SetActive(false);
        bt1.onClick.AddListener(() => BT1());
    }

    private void BT1()
    {
        panel_Exit.SetActive(false);
    }



    private void BT2()
    {
        Application.Quit();
    }
    void Start()
    {

        //获取血量条长度、高度
        //f_Panel_HPHight = img_HP.GetComponent<RectTransform>().rect.height;
        //f_Panel_HPWidth = img_HP.GetComponent<RectTransform>().rect.width;
        // f_Panel_MPHight = img_MP.GetComponent<RectTransform>().rect.height;
        // f_Panel_MPWidth = img_MP.GetComponent<RectTransform>().rect.width;


        //更新一下面板。血量条、体力条由玩家主动请求更新

        UpdateCoin();
    }

    //更新血量条
    public void UpdateHP(float hp, float hpMax)
    {
        img_HP.GetComponent<RectTransform>().sizeDelta = new Vector2(hp / hpMax * f_Panel_HPWidth, f_Panel_HPHight);
    }

    // //更新体力条
    // public void UpdateMP(float mp, float mpMax)
    // {
    //     img_MP.GetComponent<RectTransform>().sizeDelta = new Vector2(mp / mpMax * f_Panel_MPWidth, f_Panel_MPHight);
    // }


    // //更新小物品列表
    // public void UpdateItems()
    // {
    //     item_HP.text = GameInfo.Get_Item_HP().ToString();
    //     // item_MP.text = GameInfo.Get_Item_MP().ToString();
    //     item_Attack.text = GameInfo.Get_Item_Attack().ToString();
    //     item_Magic.text = GameInfo.Get_Item_Magic().ToString();
    //     item_ATKDefense.text = GameInfo.Get_Item_ATKDefense().ToString();
    //     item_MGDefense.text = GameInfo.Get_Item_MGDefense().ToString();
    // }

    //更新金币
    public void UpdateCoin()
    {
        coin.text = GameInfo.GetCoin().ToString();
    }

    //更新任务名称
    public void UpdateTaskName()
    {
        taskName.text = s_TaskName[GameInfo.GetTask()];//显示剧情任务名称
    }


    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape)) { panel_Exit.SetActive(true);
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}