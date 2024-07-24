using System.Collections.Generic;
using System.Data;
using UnityEngine;

namespace PolygonProject
{
    /// <summary>
    /// 背包数据层，提供对背包数据进行增删查改的方法
    /// </summary>
    public class BagData
    {
        #region 物品栏
        public int DefaultLCount=4;
        public int DefaultRCount=4;
        public int DefaultTCount=4;
        public int DefaultDCount=4;

        //护甲栏位
        public int ArmorHead=-1;
        public int ArmorBody=-1;
        public int ArmorLeg=-1;
        public int ArmorHand=-1;
        public int ArmorFeet=-1;
        int[] EquipItemsT;
        int[] EquipItemsD;
        int[] EquipItemsL;
        int[] EquipItemsR;
        int CurrentIndexT=0;
        int CurrentIndexD=0;
        int CurrentIndexL=0;
        int CurrentIndexR=0;
        #endregion

        //ID->物品
        Dictionary<int,Item> ItemDic=>DataBoard.Instance.ItemDic;
        //ID->背包物品
        Dictionary<int,BagItem> BagItemDic=>DataBoard.Instance.BagItemDic;
        public void Init()
        {
            EquipItemsT = new int[DefaultTCount];
            EquipItemsD = new int[DefaultDCount];
            EquipItemsL = new int[DefaultLCount];
            EquipItemsR = new int[DefaultRCount];
            for(int i = 0; i < DefaultTCount; i++)
            {
                EquipItemsT[i] = -1;
            }
            for (int i = 0; i < DefaultDCount; i++)
            {
                EquipItemsD[i] = -1;
            }
            for (int i = 0; i < DefaultLCount; i++)
            {
                EquipItemsL[i] = -1;
            }
            for (int i = 0; i < DefaultRCount; i++)
            {
                EquipItemsR[i] = -1;
            }

        }


        public Dictionary<int,BagItem> GetBagItemDic()
        {
            return BagItemDic;
        }
        public Dictionary<int,Item> GetItemDic()
        {
            return ItemDic;
        }

        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public void AddItem(int _itemID)
        {
            if(BagItemDic.ContainsKey(_itemID))
            {
                BagItemDic[_itemID].Num++;
            }
        }
        /// <summary>
        /// 增加物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public void AddItem(int _itemID,int _Num)
        {
            if(BagItemDic.ContainsKey(_itemID))
            {
                BagItemDic[_itemID].Num+=_Num;
            }
        }
        /// <summary>
        /// 移除物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public void RemoveItem(int _itemID)
        {
            if(BagItemDic.ContainsKey(_itemID))
            {
                BagItemDic[_itemID].Num--;
                if(BagItemDic[_itemID].Num==0)
                {
                    BagItemDic.Remove(_itemID);
                }
            }
        }

        /// <summary>
        /// 通过ID查找某个物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public BagItem FindItemByID(int _id)
        {


            Debug.Log("背包中未找到物品");
            return null;
        }

        /// <summary>
        /// 通过名字查找某个物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public BagItem FindItemByName(string _name)
        {

            Debug.Log("背包中未找到物品");
            return null;
        }

        public int[] GetEquippedItems(EDerection _EDerection)
        {
            switch(_EDerection)
            {
                case EDerection.Up:
                    return EquipItemsT;
                case EDerection.Down:
                    return EquipItemsD;
                case EDerection.Left:
                    return EquipItemsL;
                case EDerection.Right:
                    return EquipItemsR;
                default :
                    return null;
            }
        }

        public int GetCurrentIndex(EDerection _EDerection)
        {
            switch(_EDerection)
            {
                case EDerection.Up:
                    return CurrentIndexT;
                case EDerection.Down:
                    return CurrentIndexD;
                case EDerection.Left:
                    return CurrentIndexL;
                case EDerection.Right:
                    return CurrentIndexR;
                default :
                    return -1;
            }
        }
        public void AddArmor(int _itemID)
        {
            switch((DataBoard.Instance.BagItemDic[_itemID].item as Armor).equipType)
            {
                case EquipType.Head:
                    ArmorHead=_itemID;
                    break;
                case EquipType.Body:
                    ArmorBody=_itemID;
                    break;
                case EquipType.Leg:
                    ArmorLeg=_itemID;
                    break;
                case EquipType.Hand:
                    ArmorHand=_itemID;
                    break;
                case EquipType.Feet:
                    ArmorFeet=_itemID;
                    break;
            }
        }

        public void RemoveArmor(int _itemID)
        {
            switch((DataBoard.Instance.BagItemDic[_itemID].item as Armor).equipType)
            {
                case EquipType.Head:
                    ArmorHead=-1;
                    break;
                case EquipType.Body:
                    ArmorBody=-1;
                    break;
                case EquipType.Leg:
                    ArmorLeg=-1;
                    break;
                case EquipType.Hand:
                    ArmorHand=-1;
                    break;
                case EquipType.Feet:
                    ArmorFeet=-1;
                    break;
            }
        }
        //增加物品栏物品
        public void AddEquipItem(int _itemID,EDerection _EDerection)
        {
            switch(_EDerection)
            {
                case EDerection.Up:
                    for(int i=0;i<EquipItemsT.Length;i++)
                    {
                        if(EquipItemsT[i]==-1)
                        {
                            EquipItemsT[i]=_itemID;
                            break;
                        }
                    }
                    break;
                case EDerection.Down:
                    for(int i=0;i<EquipItemsD.Length;i++)
                    {
                        if(EquipItemsD[i]==-1)
                        {
                            EquipItemsD[i]=_itemID;
                            break;
                        }
                    }
                    break;
                case EDerection.Left:
                    for(int i=0;i<EquipItemsL.Length;i++)
                    {
                        if(EquipItemsL[i]==-1)
                        {
                            EquipItemsL[i]=_itemID;
                            break;
                        }
                    }
                    break;
                case EDerection.Right:
                    for(int i=0;i<EquipItemsR.Length;i++)
                    {
                        if(EquipItemsR[i]==-1)
                        {
                            EquipItemsR[i]=_itemID;
                            break;
                        }
                    }
                    break;
            }
        }

        //移除物品栏物品
        public void RemoveEquipItem(int _itemID,EDerection _EDerection)
        {
            switch(_EDerection)
            {
                case EDerection.Up:
                    for(int i=0;i<EquipItemsT.Length;i++)
                    {
                        if(EquipItemsT[i]==_itemID)
                        {
                            EquipItemsT[i]=-1;
                            break;
                        }
                    }
                    break;
                case EDerection.Down:
                    for(int i=0;i<EquipItemsD.Length;i++)
                    {
                        if(EquipItemsD[i]==_itemID)
                        {
                            EquipItemsD[i]=-1;
                            break;
                        }
                    }
                    break;
                case EDerection.Left:
                    for(int i=0;i<EquipItemsL.Length;i++)
                    {
                        if(EquipItemsL[i]==_itemID)
                        {
                            EquipItemsL[i]=-1;
                            break;
                        }
                    }
                    break;
                case EDerection.Right:
                    for(int i=0;i<EquipItemsR.Length;i++)
                    {
                        if(EquipItemsR[i]==_itemID)
                        {
                            EquipItemsR[i]=-1;
                            break;
                        }
                    }
                    break;
            }
        }

        public void SwitchUpItemData()
        {
            CurrentIndexT++;
            if(CurrentIndexT>=EquipItemsT.Length)
            {
                CurrentIndexT=0;
            }
        }

        public void SwitchDownItemData()
        {
            CurrentIndexD++;
            if(CurrentIndexD>=EquipItemsD.Length)
            {
                CurrentIndexD=0;
            }
        }

        public void SwitchLeftItemData()
        {
            CurrentIndexL++;
            if(CurrentIndexL>=EquipItemsL.Length)
            {
                CurrentIndexL=0;
            }
        }

        public void SwitchRightItemData()
        {
            CurrentIndexR++;
            if(CurrentIndexR>=EquipItemsR.Length)
            {
                CurrentIndexR=0;
            }
        }
    }



    public enum EItemEquipState
    {
        //没有被装备
        Unequipped,
        //被装备
        Equipped,
        //双手装备
        TwoHandEquipped,
        //左手装备
        LeftHandEquipped,
        //右手装备
        RightHandEquipped
    }
}
