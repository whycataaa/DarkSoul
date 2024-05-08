using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
///背包控制界面
/// </summary>
public class Panel_BagView : Singleton<Panel_BagView>
{
    public GameObject panel_Bag;

    [SerializeField]private GameObject bagBG_a;
    [SerializeField]private GameObject bagBG_b;
    [SerializeField]private GameObject bagBG_c;
    [SerializeField]private Button bt_a;
    [SerializeField]private Button bt_b;
    [SerializeField]private Button bt_c;


    protected override void Awake()
    {
        base.Awake();
        panel_Bag=transform.GetChild(0).gameObject;
        panel_Bag.SetActive(false);




    }
    void Update()
    {
        // Cursor.visible=panel_Bag.activeSelf;
        // if(panel_Bag.activeSelf)
        // {
        //     Cursor.lockState=CursorLockMode.Confined;
        // }
        // else
        // {
        //     Cursor.lockState=CursorLockMode.Locked;
        // }
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Cursor.visible = !Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            //切换背包的显示与隐藏
            panel_Bag.SetActive(!panel_Bag.activeSelf);
            //默认打开a背包
            SetObjectToActive(bagBG_a,bagBG_b,bagBG_c);
        }

        bt_a.onClick.AddListener(()=>SetObjectToActive(bagBG_a,bagBG_b,bagBG_c));
        bt_b.onClick.AddListener(()=>SetObjectToActive(bagBG_b,bagBG_a,bagBG_c));
        bt_c.onClick.AddListener(()=>SetObjectToActive(bagBG_c,bagBG_a,bagBG_b));
    }

    private void SetObjectToActive(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    //控制三个物体，其中一个active
    public static void SetObjectToActive(GameObject a,GameObject b,GameObject c)
    {
        a.SetActive(true);
        b.SetActive(false);
        c.SetActive(false);
    }
    //控制两个物体，其中一个active
    public static void SetObjectToActive(GameObject a,GameObject b)
    {
        a.SetActive(true);
        b.SetActive(false);
    }
    private void SetObjectToInactive(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }



}
