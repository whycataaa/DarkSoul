using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="StoreItem",menuName ="Store/StoreItem")]
public class StoreItem_SO : ScriptableObject
{
    public List<Item_SO> items=new List<Item_SO>();
}
