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
