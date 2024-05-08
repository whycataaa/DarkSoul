using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreManager : Singleton<StoreManager>
{
    //当前选中的物品
    public Item_SO selectedItem;
    public StoreGrid gridPrefab;

    public StoreItem_SO storeBuyItems;
    public StoreItem_SO storeSellItems;

    public GameObject[] store;


    public Item_SO moren;


    private void Start()
    {
        selectedItem = moren;
        Panel_StoreView.Instance.UpdateItemDetailUI();
    }
    private void OnEnable()
    {

        UpdateItemToUI();
    }

    public void UpdateItemToUI()
    {
        for (int j = 0; j < Instance.store.Length; j++)
        {
            for (int i = 0; i < Instance.store[j].transform.childCount; i++)
            {
                if (Instance.store[j].transform.GetChild(i).childCount != 0)
                {
                    //Debug.Log(bagGridControl.myBag[i].transform.GetChild(j).name);
                    DestroyImmediate(Instance.store[j].transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }
        foreach (var x in Instance.storeBuyItems.items)
        {
            StoreGrid grid_a = Instantiate(Instance.gridPrefab, FindTransToInsert(0));
            grid_a.gridImage.sprite = x.itemImage;
            grid_a.gridNum.text = x.itemNum.ToString();
            grid_a.ItemName.text = x.itemName;
        }
        foreach (var x in Instance.storeSellItems.items)
        {
            //实际使用玩家背包里的数据
            StoreGrid grid_b = Instantiate(Instance.gridPrefab, FindTransToInsert(1));
            grid_b.gridImage.sprite = x.itemImage;
            grid_b.gridNum.text = x.itemNum.ToString();
            grid_b.ItemName.text = x.itemName;
        }
    }
    private static Transform FindTransToInsert(int storeNum)
    {

        for (int i = 0; i < Instance.store[storeNum].transform.childCount; i++)
        {
            //Debug.Log(bagGridControl.myBag[bagNum].transform.GetChild(i).name);

            if (Instance.store[storeNum].transform.GetChild(i).childCount == 0)
            {
                return Instance.store[storeNum].transform.GetChild(i).transform;
            }
        }
        Debug.Log("Full Item");
        return null;
    }


}
