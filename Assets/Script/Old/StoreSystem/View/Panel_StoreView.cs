using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel_StoreView : Singleton<Panel_StoreView>
{
    public GameObject panel_Store;
    public GameObject panel_Buy,panel_Sold;

    public Button Bt_SwitchBuy,Bt_SwitchSold;
    public Button Bt_BuyItem, Bt_SoldItem;
    public Button Bt_Exit;

    [Header("物品详细面板")]
    public GameObject panel_Detail;
    public Text itemName;
    public Text info;
    public Text coinNum;
    public Image itemSprit;


    protected override void Awake()
    {
        base.Awake();


    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Bt_BuyItem.onClick.AddListener(BuyItem);
        Bt_SoldItem.onClick.AddListener(SoldItem);
        Bt_SwitchBuy.onClick.AddListener(()=> 
        {
            Panel_BagView.SetObjectToActive(panel_Buy, panel_Sold);
            Bt_BuyItem.gameObject.SetActive(true);
            Bt_SoldItem.gameObject.SetActive(false);
            StoreManager.Instance.UpdateItemToUI();
        });
        Bt_SwitchSold.onClick.AddListener(()=> 
        { 
            Panel_BagView.SetObjectToActive(panel_Sold, panel_Buy);
            Bt_BuyItem.gameObject.SetActive(false);
            Bt_SoldItem.gameObject.SetActive(true);
            StoreManager.Instance.UpdateItemToUI();
        });
        Bt_Exit.onClick.AddListener(()=>panel_Store.SetActive(false));
        panel_Store.SetActive(false);
    }

    private void SoldItem()
    {
        if(StoreManager.Instance.selectedItem.itemNum>0)
        {
            if(StoreManager.Instance.selectedItem.itemNum==0)
            {
                ItemManager.Instance.DeleteItem(StoreManager.Instance.selectedItem);
            }
            StoreManager.Instance.selectedItem.itemNum--;
            GameInfo.SetCoin(GameInfo.GetCoin() + StoreManager.Instance.selectedItem.price);
        }
        else
        {
            return;
        }
        StoreManager.Instance.UpdateItemToUI();
    }

    private void BuyItem()
    {
        if(GameInfo.GetCoin()>=StoreManager.Instance.selectedItem.price&&StoreManager.Instance.selectedItem.itemNum>0)
        {
            GameInfo.SetCoin(GameInfo.GetCoin()- StoreManager.Instance.selectedItem.price);

            StoreManager.Instance.selectedItem.itemNum--;
            if(StoreManager.Instance.selectedItem.itemNum==0)
            {
                StoreManager.Instance.storeBuyItems.items.Remove(StoreManager.Instance.selectedItem);
            }
            StoreManager.Instance.UpdateItemToUI();
            if(StoreManager.Instance.selectedItem.storeTobag!=null)
            {
                ItemManager.Instance.AddItem(StoreManager.Instance.selectedItem.storeTobag);
            }
            else
            {
                ItemManager.Instance.AddItem(StoreManager.Instance.selectedItem);
            }
            
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 打开商店UI
    /// </summary>
    public void OpenStoreUI()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        panel_Store.SetActive(true) ;
        Panel_BagView.SetObjectToActive(panel_Buy, panel_Sold);
        Bt_BuyItem.gameObject.SetActive(true);
        Bt_SoldItem.gameObject.SetActive(false);
    }
/*
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M)) {
        OpenStoreUI();
        }
    }
*/
    /// <summary>
    /// 更新商店右侧的详细显示
    /// </summary>
    public void UpdateItemDetailUI()
    {
            info.text = StoreManager.Instance.selectedItem.itemInfo;
            coinNum.text = StoreManager.Instance.selectedItem.price.ToString();
            itemSprit.sprite = StoreManager.Instance.selectedItem.itemImage;
            itemName.text = StoreManager.Instance.selectedItem.itemName;
    }

}
