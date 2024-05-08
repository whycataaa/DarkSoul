using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///对背包和场景物品进行管理
/// </summary>
public class ItemManager : Singleton<ItemManager>
{
    //背包的物品数据仓库
    public ItemList_SO bagItem;

    public StoreItem_SO buyItems;
    public StoreItem_SO SoldItems;
    public void AddItem(Item_SO item)
    {
        // switch(item.itemType)
        // {
        //     case 0:
        //     if(!bagItem.bag_A.Contains(item))
        //     {
        //         bagItem.bag_A.Add(item);
        //     }
        //     item.itemNum++;
        //     break;

        //     case (Item_SO.ItemType)1:
        //     if(!bagItem.bag_B.Contains(item))
        //     {
        //         bagItem.bag_B.Add(item);
        //     }
        //     item.itemNum++;
        //     break;

        //     case (Item_SO.ItemType)2:
        //     if(!bagItem.bag_C.Contains(item))
        //     {
        //         bagItem.bag_C.Add(item);
        //     }
        //     item.itemNum++;
        //     break;

        // }
        if(!bagItem.bagItems.Contains(item)) 
        {
            bagItem.bagItems.Add(item);
        }
        toastUI.Instance.Showtoast("获得了" + item.name);
        item.itemNum++;
        BagGridControl.UpdateItemToUI();
    }

    public void DeleteItem(Item_SO item)
    {
        // switch(item.itemType)
        // {
        //     case 0:
        //         foreach(var x in bagItem.bag_A)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 x.itemNum--;
        //                 if(x.itemNum==0)
        //                 {
        //                     bagItem.bag_A.Remove(x);
        //                 }
        //             }
        //         }
        //         break;
            
        //     case (Item_SO.ItemType)1:
        //         foreach(var x in bagItem.bag_B)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 x.itemNum--;
        //                 if(x.itemNum==0)
        //                 {
        //                     bagItem.bag_A.Remove(x);
        //                 }
        //             }
        //         }
        //         break;
            
        //     case (Item_SO.ItemType)2:
        //         foreach(var x in bagItem.bag_C)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 x.itemNum--;
        //                 if(x.itemNum==0)
        //                 {
        //                     bagItem.bag_A.Remove(x);
        //                 }
        //             }
        //         }
        //         break;
        // }
            foreach(var x in bagItem.bagItems)
            {
                if(item.itemName==x.itemName)
                {
                    x.itemNum--;
                    if(x.itemNum==0)
                    {
                        bagItem.bagItems.Remove(x);
                    }
                }
            }
        BagGridControl.UpdateItemToUI();
    }
    /// <summary>
    /// 查询背包中某个物品的个数
    /// </summary>
    /// <param name="item_SO"></param>
    /// <returns></returns>
    public int FindItemNum(Item_SO item)
    {
        // switch(item.itemType)
        // {
        //     case 0:
        //         foreach(var x in bagItem.bag_A)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 return x.itemNum;
        //             }
        //             else
        //             {
        //                 return 0;
        //             }
        //         }
        //         break;
            
        //     case (Item_SO.ItemType)1:
        //         foreach(var x in bagItem.bag_B)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 return x.itemNum;
        //             }
        //             else
        //             {
        //                 return 0;
        //             }
        //         }
        //         break;
            
        //     case (Item_SO.ItemType)2:
        //         foreach(var x in bagItem.bag_C)
        //         {
        //             if(item.itemName==x.itemName)
        //             {
        //                 return x.itemNum;
        //             }
        //             else
        //             {
        //                 return 0;
        //             }
        //         }
        //         break;

        // }
        // return 0;


        foreach(var x in bagItem.bagItems)
        {
            if(item.itemName==x.itemName)
            {
                return x.itemNum;
            }
            else
            {
                return 0;
            }
        }
        return 0;
    }

    public Item_SO FindItem(string name)
    {
        foreach(var x in bagItem.bagItems)
        {
            if(x.itemName==name)
                return x;
        }
        return null;
    }
    public Item_SO FindBuyItem(string name)
    {
        foreach (var x in buyItems.items)
        {
            if (x.itemName == name)
                return x;
        }
        return null;
    }
    public Item_SO FindSoldItem(string name)
    {
        foreach (var x in SoldItems.items)
        {
            if (x.itemName == name)
                return x;
        }
        return null;
    }
}
