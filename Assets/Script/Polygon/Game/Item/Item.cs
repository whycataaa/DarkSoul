using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class Item
    {
        public int id;
        public string name;
        public string info;
        public Sprite sprite;
        public ItemType itemType;
        public int MaxStackCount
        {
            get
            {
                switch(itemType)
                {
                    case ItemType.Weapon:
                        return 1;
                    case ItemType.Useable:
                        return 100;
                    case ItemType.Spell:
                        return 1;
                    case ItemType.Collection:
                        return 100;
                    default:
                        return 1;
                }
            }
        }

    }



    public enum ItemType
    {
        //可装备
        Weapon=0,
        //可使用的道具
        Useable=1,
        //法术
        Spell=2,
        //不可使用的收集物
        Collection=3
    }
}
