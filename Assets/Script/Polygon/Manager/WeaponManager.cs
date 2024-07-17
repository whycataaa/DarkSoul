using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.Animations;

namespace PolygonProject
{
    public enum HandState
    {
        None,
        LeftHand,
        RightHand,
        TwoHands
    }

    /// <summary>
    /// 管理已经装备的武器
    /// </summary>
    public class WeaponManager
    {
        //当前持武器状态
        public HandState CurrentHandState;
        //当前武器序号
        public int currentWeaponIndexL=0;
        public int currentWeaponIndexR=0;
        //最大武器数量
        private int maxWeaponCountL=4;
        private int maxWeaponCountR=4;
        //当前装备的武器
        private int currentWeaponLID=-1;
        private int currentWeaponRID=-1;

        //左手可装备的武器
        public List<int> weaponsL;
        //右手可装备的武器
        public List<int> weaponsR;

        //绑定武器的位置
        Transform weaponTransL;

        Transform weaponTransR;
        //是否在检测
        [SerializeField]public bool OnDetect=false;
        //武器列表
        [SerializeField]List<Weapon> weapons=new List<Weapon>();
        //攻击检测的列表
        [SerializeField]List<Detection> detections=new List<Detection>();

        public Dictionary<int,Weapon> weaponDic;

        public GameObject player;
        public BagData bagData;
        public int AttackTimes=1;
        public void Init()
        {
            maxWeaponCountL=4;
            maxWeaponCountR=4;
            weaponsR=new List<int>(maxWeaponCountR);
            weaponsL=new List<int>(maxWeaponCountL);
            weaponDic=new Dictionary<int, Weapon>();

            //读取武器表，加载数据
            var dt = CSVTool.OpenCSV("武器表");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                int id=0;
                int attack=0;
                int defense=0;
                int left=0;
                int right=0;
                int twoHands=0;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    switch (j)
                    {
                        case 0:
                            id = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 1:
                            attack = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 2:
                            defense = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 3:
                            left = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 4:
                            right = int.Parse(dt.Rows[i][j].ToString());
                            break;
                        case 5:
                            twoHands = int.Parse(dt.Rows[i][j].ToString());
                            break;
                    }
                }
                Weapon weapon=new Weapon(id,attack,defense,left,right,twoHands);
                weaponDic.Add(id,weapon);
                if(bagData.GetItemDic().ContainsKey(id))
                {
                    weapon.info=bagData.GetItemDic()[id].info;
                    weapon.itemType=bagData.GetItemDic()[id].itemType;
                    weapon.name=bagData.GetItemDic()[id].name;
                    weapon.sprite=bagData.GetItemDic()[id].sprite;
                }

            }


            CurrentHandState=HandState.None;
            weaponTransL=TransformHelper.FindDeepTransform<Transform>(player.transform,"WeaponSlotL");
            weaponTransR=TransformHelper.FindDeepTransform<Transform>(player.transform,"WeaponSlotR");

            EventManager.Instance.AddListener(EventName.EquipWeapon,AddWeapon);

            Debug.Log(weaponTransL);
            Debug.Log(weaponTransR);
        }

        /// <summary>
        /// 在指定位置销毁生成指定武器
        /// </summary>
        /// <param name="_index"></param>
        public void EquipWeapon(int _index,bool IsLeft)
        {
            //换左手武器
            if(IsLeft)
            {
                CurrentHandState=HandState.LeftHand;
                
                if(weaponTransL.childCount>0)
                {
                    GameObject.Destroy(weaponTransL.GetChild(0).gameObject);
                    var weaponPre=ResManager.Instance.LoadResource<GameObject>
                    ("Item/Weapon","Weapon_"+weaponDic[weaponsL[_index]].id.ToString().PadLeft(4,'0')+".prefab");
                    var weapon=GameObject.Instantiate(weaponPre);
                    weapon.name=weaponPre.name;
                    weapon.transform.SetParent(weaponTransL.transform);
                    weapon.transform.localPosition=Vector3.zero;
                }
                else
                {
                    var weaponPre=ResManager.Instance.LoadResource<GameObject>
                    ("Item/Weapon","Weapon_"+weaponDic[weaponsL[_index]].id.ToString().PadLeft(4,'0')+".prefab");
                    var weapon=GameObject.Instantiate(weaponPre);
                    weapon.name=weaponPre.name;
                    weapon.transform.SetParent(weaponTransL);
                    weapon.transform.localPosition=new Vector3(0,0,0);
                }
            }
            //换右手武器
            else
            {
                CurrentHandState=HandState.RightHand;
                if(weaponTransR.childCount>0)
                {
                    GameObject.Destroy(weaponTransR.GetChild(0).gameObject);
                    var weaponPre=ResManager.Instance.LoadResource<GameObject>
                    ("Item/Weapon","Weapon_"+weaponDic[weaponsR[_index]].id.ToString().PadLeft(4,'0')+".prefab");
                   var weapon=GameObject.Instantiate(weaponPre);
                   weapon.name=weaponPre.name;
                   weapon.transform.SetParent(weaponTransR.transform);
                   weapon.transform.localPosition=new Vector3(0,0,0);

                }
                else
                {
                    var weaponPre=ResManager.Instance.LoadResource<GameObject>
                    ("Item/Weapon","Weapon_"+weaponDic[weaponsR[_index]].id.ToString().PadLeft(4,'0')+".prefab");
                    var weapon=GameObject.Instantiate(weaponPre);
                    weapon.name=weaponPre.name;
                    weapon.transform.SetParent(weaponTransR);
                    weapon.transform.localPosition=new Vector3(0,0,0);
                }
            }
        }


        public void AddWeapon(object sender, EventArgs e)
        {
            var data = e as ItemEventArgs;
            CurrentHandState=data.handState;

            Debug.Log($"已添加{DataBoard.Instance.BagData.GetBagItemDic()[data.BagItemID].item.name}");
            switch(data.handState)
            {
                case HandState.LeftHand:
                    if(weaponsL.Count<maxWeaponCountL)
                    weaponsL.Add(data.BagItemID);
                    break;
                case HandState.RightHand:
                    if(weaponsR.Count<maxWeaponCountR)
                    weaponsR.Add(data.BagItemID);
                    break;
                case HandState.TwoHands:
                    if(weaponsL.Count<maxWeaponCountL)
                    weaponsL.Add(data.BagItemID);
                    if(weaponsR.Count<maxWeaponCountR)
                    weaponsR.Add(data.BagItemID);
                    break;
                case HandState.None:
                    if(weaponsL.Contains(data.BagItemID))
                    weaponsL.Remove(data.BagItemID);
                    if(weaponsR.Contains(data.BagItemID))
                    weaponsR.Remove(data.BagItemID);
                    break;
            }

        }
        public Weapon GetCurrentWeapon(bool IsLeft)
        {
            if(IsLeft)
            {
                if(currentWeaponLID!=-1)
                {
                    return weaponDic[currentWeaponLID];
                }
                else
                {
                    return null;
                }
            }
            else
            {
                if(currentWeaponRID!=-1)
                {
                    return weaponDic[currentWeaponRID];
                }
                else
                {
                    return null;
                }
            }
        }

        public int GetCurrentWeaponMaxAttackTimes(bool IsLeft)
        {
            //返回左手武器
            if(IsLeft)
            {
                if(currentWeaponLID!=-1)
                {
                    return weaponDic[currentWeaponLID].LeftHandAttackTimes;
                }
                else
                {
                    return 6;
                }
            }
            else
            {
                if(currentWeaponRID!=-1)
                {
                    return weaponDic[currentWeaponRID].RightHandAttackTimes;
                }
                else
                {
                    return 6;
                }
            }

        }

        /// <summary>
        /// 通过索引设置当前武器
        /// </summary>
        /// <param name="_index"></param>
        /// <param name="IsLeft"></param>
        public void SetCurrentWeapon(int _index,bool IsLeft)
        {
            if(IsLeft)
            {
                currentWeaponLID=weaponsL[_index];
            }
            else
            {
                currentWeaponRID=weaponsR[_index];
            }
        }

        public Dictionary<int,Weapon> GetWeaponDic()
        {
            return weaponDic;
        }
    }
}
