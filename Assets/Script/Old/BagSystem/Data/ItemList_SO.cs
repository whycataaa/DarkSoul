using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///管理背包中的物体
/// </summary>
[CreateAssetMenu(fileName ="New BagItem",menuName ="Bag/New ItemList")]
public class ItemList_SO : ScriptableObject
{
    public List<Item_SO> bagItems=new List<Item_SO>();
    // public List<Item_SO> bag_B=new List<Item_SO>();
    // public List<Item_SO> bag_C=new List<Item_SO>();

}
