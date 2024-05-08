using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///控制每个背包格子
/// </summary>
public class BagGridControl : Singleton<BagGridControl>
{
    //背包数据仓库、格子中物体预制体、和UI中显示物体元素的父元素
    public ItemList_SO bagItem;
    public BagGrid gridPrefab;
    public GameObject[] myBag;

    //每次打开背包，动态的更新背包UI元素
    private void OnEnable()
    {
        UpdateItemToUI();
    }

    /// <summary>
    /// 在UI中将一个物体的数据仓库显示出来
    /// </summary>
    /// <param name="item"></param>
    public static void insertItemToUI(Item_SO item)
    {
        switch(item.itemType)
        {
            case 0:
            BagGrid grid_a = Instantiate(Instance.gridPrefab,FindTransToInsert(0));
            grid_a.gridImage.sprite = item.itemImage;
            grid_a.gridNum.text = item.itemNum.ToString();
            grid_a.ItemName = item.itemName.ToString();
            break;

            case (Item_SO.ItemType)1:
            BagGrid grid_b = Instantiate(Instance.gridPrefab,FindTransToInsert(1));
            grid_b.gridImage.sprite = item.itemImage;
            grid_b.gridNum.text = item.itemNum.ToString();
            grid_b.ItemName = item.itemName.ToString();
            break;

            case (Item_SO.ItemType)2:
            BagGrid grid_c = Instantiate(Instance.gridPrefab,FindTransToInsert(2));
            grid_c.gridImage.sprite = item.itemImage;
            grid_c.gridNum.text = item.itemNum.ToString();
            grid_c.ItemName = item.itemName.ToString();
            break;

        }
    }

    private static Transform FindTransToInsert(int bagNum)
    {

        for(int i=0;i<Instance.myBag[bagNum].transform.childCount;i++)
        {
            //Debug.Log(bagGridControl.myBag[bagNum].transform.GetChild(i).name);

            if(Instance.myBag[bagNum].transform.GetChild(i).childCount==0)
            {
                return Instance.myBag[bagNum].transform.GetChild(i).transform;
            }
        }
        Debug.Log("Full Item");
        return null;
    }
    /// <summary>
    /// 将背包数据仓库中所有物体显示在UI上
    /// </summary>
    public static void UpdateItemToUI()
    {
        for(int j=0;j<Instance.myBag.Length;j++)
        {
            for (int i = 0; i < Instance.myBag[j].transform.childCount; i++)
            {
                if(Instance.myBag[j].transform.GetChild(i).childCount!=0)
                {
                    //Debug.Log(bagGridControl.myBag[i].transform.GetChild(j).name);
                    DestroyImmediate(Instance.myBag[j].transform.GetChild(i).GetChild(0).gameObject);
                }
            }
        }
            foreach(var x in Instance.bagItem.bagItems)
            {
                insertItemToUI(x);
            }



    }
}
