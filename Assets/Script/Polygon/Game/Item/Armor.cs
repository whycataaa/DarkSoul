using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    //护甲
    public class Armor : Item
    {
        //装备部位
        public EquipType equipType;
        public Armor()
        {
            ItemType=ItemType.Armor;
        }

    }

    public enum EquipType
    {
        Head,
        Body,
        Hand,
        Leg,
        Feet
    }
}
