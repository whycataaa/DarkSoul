using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    //管理物品的生成以及附加属性
    public class ItemManager : SingletonMono<ItemManager>
    {
        readonly int MaxCommonAttributeCount=1;
        readonly int MaxRareAttributeCount=2;
        readonly int MaxEpicAttributeCount=3;
        readonly int MaxLegendaryAttributeCount=4;
        //初始化词条武器
        public BagItem InitItem(int _ItemID,EQuality _Quality)
        {

            Debug.Log(DataBoard.Instance.ItemDic[_ItemID].ItemType);
            switch(DataBoard.Instance.ItemDic[_ItemID].ItemType)
            {
                case ItemType.Weapon:
                    return InitWeapon(_ItemID,_Quality);
                case ItemType.Armor:
                    return InitArmor(_ItemID,_Quality);
                case ItemType.Useable:
                    return InitUseable(_ItemID,_Quality);
                case ItemType.Spell:
                    return InitSpell(_ItemID,_Quality);
                case ItemType.Collection:
                    return InitCollection(_ItemID,_Quality);
            }

            Debug.Log("初始化词条失败");
            return null;
        }

        private BagItem InitCollection(int _ItemID,EQuality _Quality)
        {
            throw new System.NotImplementedException();
        }

        private BagItem InitSpell(int _ItemID,EQuality _Quality)
        {
            throw new System.NotImplementedException();
        }

        private BagItem InitUseable(int _ItemID,EQuality _Quality)
        {
            throw new System.NotImplementedException();
        }

        private BagItem InitArmor(int _ItemID,EQuality _Quality)
        {
            throw new System.NotImplementedException();
        }

        private BagItem InitWeapon(int _ItemID,EQuality _Quality)
        {
            BagItem bagWeapon=new BagItem(DataBoard.Instance.ItemDic[_ItemID] as Weapon,1);
            bagWeapon.item.Quality=_Quality;
            RandomAttribute(bagWeapon,_Quality);
            bagWeapon.item.ItemState=EItemIdentifyState.Identified;
            return bagWeapon;
        }


        private BagItem RandomAttribute(BagItem _BagItem,EQuality _Quality)
        {
            int x=0;
            switch(_Quality)
            {
                case EQuality.Common:
                    x=MaxCommonAttributeCount;
                    break;
                case EQuality.Epic:
                    x=MaxEpicAttributeCount;
                    break;
                case EQuality.Rare:
                    x=MaxRareAttributeCount;
                    break;
                case EQuality.Legendary:
                    x=MaxLegendaryAttributeCount;
                    break;
            }
            for(int i=0;i<x;i++)
            {
                int num=Random.Range(12,15);
                Debug.Log(num);
                switch((EAttribute)num)
                {
                    case EAttribute.AddPhysicalStrengthBonus:
                        if(!_BagItem.AttributeDic.ContainsKey((EAttribute)num))
                        {
                            _BagItem.AttributeDic.Add((EAttribute)num,Random.Range(0.01f,10));
                        }
                        else
                        {
                            _BagItem.AttributeDic[(EAttribute)num]+=Random.Range(0.01f,10);
                        }
                        break;
                    case EAttribute.AddMagicStrengthBonus:
                        if(!_BagItem.AttributeDic.ContainsKey((EAttribute)num))
                        {
                            _BagItem.AttributeDic.Add((EAttribute)num,Random.Range(0.01f,10));
                        }
                        else
                        {
                            _BagItem.AttributeDic[(EAttribute)num]+=Random.Range(0.01f,10);
                        }
                        break;
                    case EAttribute.AddWeaponDamage:
                        if(!_BagItem.AttributeDic.ContainsKey((EAttribute)num))
                        {
                            _BagItem.AttributeDic.Add((EAttribute)num,Random.Range(1,3));
                        }
                        else
                        {
                            _BagItem.AttributeDic[(EAttribute)num]+=Random.Range(1,3);
                        }
                        break;
                }

            }

            return _BagItem;
        }
    }
}
