using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{

    public class DataBoard:Singleton<DataBoard>
    {
        [Serializable]
        class BagItemData
        {
            public Dictionary<int,BagItem> BagItemDic=new Dictionary<int,BagItem>();
        }
        BagItemData bagItemData;
        public BagData BagData;
        //ID->背包物品
        public Dictionary<int,BagItem> BagItemDic;

        public Dictionary<int,Item> ItemDic;

        public void Init()
        {
            BagItemDic = new Dictionary<int, BagItem>();
            ItemDic = new Dictionary<int, Item>();
            bagItemData = new BagItemData();


            /// <summary>
            /// 从CSV中加载数据
            /// </summary>
            LoadWeapon();
            LoadUseable();
            LoadBagData();
            //LoadArmor();
            //LoadSpell();
        }

        public void Save()
        {
            if(bagItemData==null)
            {
                bagItemData = new BagItemData();
            }
            bagItemData.BagItemDic=BagItemDic;
            SaveSystem.SaveByJson("BagItemData", bagItemData);
        }
        public void Load()
        {
            bagItemData = SaveSystem.LoadFromJson<BagItemData>("BagItemData");
        }
        private void LoadBagData()
        {
            bagItemData = SaveSystem.LoadFromJson<BagItemData>("BagItemData");
            if(bagItemData!=null)
            {
                BagItemDic=bagItemData.BagItemDic;
            }

        }

        private void LoadUseable()
        {
            var itemDt = CSVTool.OpenCSV("消耗品表");
            for (int i = 0; i < itemDt.Rows.Count; i++)
            {
                Useable item = new Useable();
                for (int j = 0; j < itemDt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            item.ID = int.Parse(itemDt.Rows[i][j].ToString());
                            break;
                        case 1:
                            item.Name = itemDt.Rows[i][j].ToString();
                            break;
                        case 2:
                            item.Info = itemDt.Rows[i][j].ToString();
                            break;
                        case 3:
                            item.IconID=itemDt.Rows[i][j].ToString();
                            break;
                    }
                }
                //加到字典里
                item.ItemType = ItemType.Useable;
                ItemDic.Add(item.ID, item);
                Debug.Log("已添加："+item.Name+"ID:"+item.ID);

        }

        }

        private void LoadWeapon()
        {

            //读取武器表，加载数据
            var dt = CSVTool.OpenCSV("武器表");
            for (int i = 0; i < dt.Rows.Count; i++)
            {

                int _ID = 0;
                string _Name = "";
                string _Info = "";
                int _MaxStackCount = 0;

                float _BaseDamage = 0;
                float _MoveSpeed = 0;
                List<int> _AttackMultiplier = new List<int>();
                int _ImpactPower = 0;
                int _HitMoveSpeed = 0;
                int _HitMoveSpeedTime = 0;
                EQuality _Quality = 0;
                string _IconID = "";
                int _BaseFireDamage = 0;
                int _BaseIceDamage = 0;
                int _BaseElectricDamage = 0;
                int _BaseHolyDamage = 0;
                int _BaseShadowDamage = 0;
                int _BaseArithmeticDamage = 0;



                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            _ID = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 1:
                            _Name = dt.Rows[i][j].ToString();
                            break;
                        case 2:
                            _Info = dt.Rows[i][j].ToString();
                            break;
                        case 3:
                            _MaxStackCount = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 4:
                            _BaseDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 5:
                            string s = dt.Rows[i][j].ToString();
                            string[] ss = s.Split('|');
                            foreach (string _s in ss)
                            {
                                _AttackMultiplier.Add(int.Parse(_s));
                            }
                            break;
                        case 6:
                            _MoveSpeed = float.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 7:
                            _ImpactPower = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 8:
                            string a = dt.Rows[i][j].ToString();
                            string[] aa = a.Split('|');

                            _HitMoveSpeed=int.Parse(aa[0]);
                            _HitMoveSpeedTime = int.Parse(aa[1]);

                            break;
                        case 9:
                            _Quality = (EQuality)int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 10:
                            _IconID = dt.Rows[i][j].ToString();
                            break;
                        case 11:
                            _BaseFireDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 12:
                            _BaseIceDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 13:
                            _BaseElectricDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 14:
                            _BaseHolyDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 15:
                            _BaseShadowDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 16:
                            _BaseArithmeticDamage = int.Parse(dt.Rows[i][j].ToString());
                            break;
                    }
                }
                Weapon weapon = new Weapon
                (
                    _ID,
                    _Name,
                    _Info,
                    _MaxStackCount,
                    _BaseDamage,
                    _MoveSpeed,
                    _AttackMultiplier,
                    _ImpactPower,
                    _HitMoveSpeed,
                    _HitMoveSpeedTime,
                    _Quality,
                    _IconID,
                    _BaseFireDamage,
                    _BaseIceDamage,
                    _BaseElectricDamage,
                    _BaseHolyDamage,
                    _BaseShadowDamage,
                    _BaseArithmeticDamage
                );

                var data = SaveSystem.LoadFromJson<SaveData>("WeaponRotationData").Rotations.Find(x => x.ID == _ID);
                weapon.DefaultRotationL = data.RotationL;
                weapon.DefaultRotationR = data.RotationR;
//                Debug.Log(data.RotationL);
//                Debug.Log(data.RotationR);
                weapon.ItemType = ItemType.Weapon;

                ItemDic.Add(_ID, weapon);
                Debug.Log("已添加："+weapon.Name+"ID:"+_ID);

            }
        }

        private void LoadSpell()
        {
            var itemDt = CSVTool.OpenCSV("法术表");
            for (int i = 0; i < itemDt.Rows.Count; i++)
            {
                Spell item = new Spell();
                for (int j = 0; j < itemDt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            item.ID = int.Parse(itemDt.Rows[i][j].ToString());
                            break;
                        case 1:
                            item.ItemType = (ItemType)int.Parse(itemDt.Rows[i][j].ToString());
                            Debug.Log(item.ItemType);
                            break;
                        case 2:
                            item.Name = itemDt.Rows[i][j].ToString();
                            break;
                        case 3:
                            item.Info = itemDt.Rows[i][j].ToString();
                            break;
                        case 4:
                            item.IconID= itemDt.Rows[i][j].ToString();
                            break;
                    }
                }

                item.ItemType = ItemType.Spell;
                //加到字典里
                ItemDic.Add(item.ID, item);


            }
        }

        private void LoadArmor()
        {
            var itemDt = CSVTool.OpenCSV("护甲表");
            for (int i = 0; i < itemDt.Rows.Count; i++)
            {
                Armor item = new Armor();
                for (int j = 0; j < itemDt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            item.ID = int.Parse(itemDt.Rows[i][j].ToString());
                            break;
                        case 1:
                            item.ItemType = (ItemType)int.Parse(itemDt.Rows[i][j].ToString());
                            Debug.Log(item.ItemType);
                            break;
                        case 2:
                            item.Name = itemDt.Rows[i][j].ToString();
                            break;
                        case 3:
                            item.Info = itemDt.Rows[i][j].ToString();
                            break;
                        case 4:
                            item.IconID= itemDt.Rows[i][j].ToString();
                            break;
                    }
                }


                item.ItemType = ItemType.Armor;
                //加到字典里
                ItemDic.Add(item.ID, item);

            }
        }

        [System.Serializable]
        class SaveData
        {
            public List<WeaponData> Rotations;
            public SaveData()
            {
                Rotations=new List<WeaponData>();
            }
        }

        [System.Serializable]
        struct WeaponData
        {
            public int ID;
            public Quaternion RotationL;
            public Quaternion RotationR;
        }

    }
}
