using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreGrid : MonoBehaviour
{
    public Image gridImage;
    public Text gridNum;
    public Text ItemName;
    public Button Bt_SelectItem;

    private void Awake()
    {
        Bt_SelectItem=GetComponent<Button>();
        Bt_SelectItem.onClick.AddListener(SelectItem);
    }

    private void SelectItem()
    {
        if(Panel_StoreView.Instance.panel_Buy.activeSelf)
        {
            StoreManager.Instance.selectedItem=ItemManager.Instance.FindBuyItem(ItemName.text);
        }
        else if(Panel_StoreView.Instance.panel_Sold.activeSelf)
        {
            StoreManager.Instance.selectedItem = ItemManager.Instance.FindSoldItem(ItemName.text);
        }
        
        Debug.Log(StoreManager.Instance.selectedItem);
        Panel_StoreView.Instance.UpdateItemDetailUI();
    }
}
