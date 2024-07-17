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
