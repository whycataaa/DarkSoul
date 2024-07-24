using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public interface IItem
    {
        int ID{get;set;}
        string Name{get;set;}
        string Info{get;set;}
        string IconID{get;set;}
        EQuality Quality{get;set;}
        int MaxStackCount{get;set;}
        EItemIdentifyState ItemState{get;set;}
        ItemType ItemType{get;set;}

        Sprite GetSprite();
    }
    //物品基类
    public abstract class Item:IItem
    {
        public int ID{get;set;}
        public string Name{get;set;}
        public string Info{get;set;}
        public string IconID{get;set;}
        public ItemType ItemType{get;set;}
        public int MaxStackCount{get;set;}
        public EQuality Quality{get;set;}
        public EItemIdentifyState ItemState{get;set;}

        public Sprite GetSprite()
        {
            return ResManager.Instance.LoadResource<Sprite>("Icon",IconID+ ".png");
        }

    }

    public enum EItemIdentifyState
    {
        //已鉴定
        Identified,
        //未鉴定
        Unidentified
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
        Collection=3,
        //护甲
        Armor = 4
    }
}
