using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

namespace PolygonProject
{
    /// <summary>
    /// 背包数据层，提供对背包数据进行增删查改的方法
    /// </summary>
    public class BagData
    {
        public int DefaultLCount=4;
        public int DefaultRCount=4;
        public int DefaultTCount=4;
        public int DefaultDCount=4;

        int[] EquipItemsT;
        int[] EquipItemsD;
        int[] EquipItemsL;
        int[] EquipItemsR;
        int CurrentIndexT=0;
        int CurrentIndexD=0;
        int CurrentIndexL=0;
        int CurrentIndexR=0;

        int currentItemUp=>EquipItemsT[CurrentIndexT];
        int currentItemDown=>EquipItemsD[CurrentIndexD];
        int currentItemLeft=>EquipItemsL[CurrentIndexL];
        int currentItemRight=>EquipItemsR[CurrentIndexR];
        BagPanelManager bagPanelManager;
        public BagData(BagPanelManager _bagPanelManager)
        {
            bagPanelManager=_bagPanelManager;
        }
        //存放在背包中的物品
        List<BagItem> bagItems;

        //ID->物品
        Dictionary<int,Item> ItemDic;
        //ID->背包物品
        Dictionary<int,BagItem> BagItemDic;
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
            LoadDataFromCSV();
        }

        /// <summary>
        /// 从CSV中加载数据
        /// </summary>
        private void LoadDataFromCSV()
        {
            BagItemDic=new Dictionary<int, BagItem>();
            ItemDic = new Dictionary<int, Item>();
            var itemDt = CSVTool.OpenCSV("物品表");
            for (int i = 0; i < itemDt.Rows.Count; i++)
            {
                Item item = new Item();
                for (int j = 0; j < itemDt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            item.id = int.Parse(itemDt.Rows[i][j].ToString());
                            break;
                        case 1:
                            item.itemType = (ItemType)int.Parse(itemDt.Rows[i][j].ToString());
                            Debug.Log(item.itemType);
                            break;
                        case 2:
                            item.name = itemDt.Rows[i][j].ToString();
                            break;
                        case 3:
                            item.info = itemDt.Rows[i][j].ToString();
                            break;
                    }
                }

                //物品名称用#0000表示
                string itemResName = "#" + item.id.ToString().PadLeft(4, '0');
                item.sprite = ResManager.Instance.LoadResource<Sprite>("Icon", itemResName + ".png");
                //加到字典里
                ItemDic.Add(item.id, item);
            }

            bagItems = new List<BagItem>();

            var dt = CSVTool.OpenCSV("玩家背包表");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id = -1;
                int num = 0;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            id = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 1:
                            num = int.Parse(dt.Rows[i][j].ToString());
                            break;
                    }
                }
                BagItem bagItem=new BagItem(ItemDic[id], num);
                bagItems.Add(bagItem);

                BagItemDic.Add(id,bagItem);
            }
        }

        public List<BagItem> GetBagItems()
        {
            return bagItems;
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
            else
            {
                var bagItem=new BagItem(ItemDic[_itemID],1);
                bagItems.Add(bagItem);
                BagItemDic.Add(_itemID,bagItem);
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
                    bagItems.Remove(BagItemDic[_itemID]);
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
            foreach(var bagItem in bagItems)
            {
                if(bagItem.item.id==_id)
                {
                    return bagItem;
                }
            }

            Debug.Log("背包中未找到物品");
            return null;
        }

        /// <summary>
        /// 通过名字查找某个物品
        /// </summary>
        /// <param name="_bagItem"></param>
        public BagItem FindItemByName(string _name)
        {
            foreach(var bagItem in bagItems)
            {
                if(bagItem.item.name==_name)
                {
                    return bagItem;
                }
            }
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
    public class BagItem
    {
        public BagItem(Item _item,int _Num)
        {
            item=_item;
            Num=_Num;
        }
        public Item item;
        public int Num;
        //默认没有被装备
        public ItemState itemState=ItemState.Unequipped;
    }


    public enum ItemState
    {
        //没有被装备
        Unequipped,
        //被装备
        Equipped
    }
}
