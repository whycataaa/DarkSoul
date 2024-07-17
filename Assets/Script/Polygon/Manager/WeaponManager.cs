using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

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
    /// 管理已经装备的武器(数据和实体)
    /// </summary>
    public class WeaponManager
    {

        //当前持武器状态
        private HandState CurrentHandState;
        //当前武器序号
        private int currentWeaponIndexL=0;
        private int currentWeaponIndexR=0;
        //最大武器数量
        private int maxWeaponCountL=4;
        private int maxWeaponCountR=4;
        //当前装备的武器
        private int currentWeaponLID=>weaponsL[currentWeaponIndexL];
        private int currentWeaponRID=>weaponsR[currentWeaponIndexR];

        //左手可装备的武器
        private int[] weaponsL;
        //右手可装备的武器
        private int[] weaponsR;
        //存储武器默认的旋转
        private Dictionary<int,Vector3> WeaponDefaultRotation;

        //绑定武器的位置
        Transform weaponTransL;

        Transform weaponTransR;
        //是否在检测
        [SerializeField]public bool OnDetect=false;
        //武器列表
        [SerializeField]List<Weapon> weapons=new List<Weapon>();
        //攻击检测的列表
        [SerializeField]List<Detection> detections=new List<Detection>();

        private Dictionary<int,Weapon> weaponDic;

        public GameObject player;
        public BagData bagData;
        public int AttackTimesL=1;
        public int AttackTimesR=1;
        public void Init()
        {
            Button button=GameObject.Find("ButtonSave").GetComponent<Button>();
            button.onClick.AddListener(AddData);
            Button button2=GameObject.Find("ButtonSaveAll").GetComponent<Button>();
            button2.onClick.AddListener(Save);
            button.gameObject.SetActive(false);
            button2.gameObject.SetActive(false);
            Debug.Log(Application.persistentDataPath);
            maxWeaponCountL=4;
            maxWeaponCountR=4;
            weaponsL=new int[maxWeaponCountL];
            weaponsR=new int[maxWeaponCountR];
            for(int i=0;i<maxWeaponCountL;i++)
            {
                weaponsL[i]=-1;
            }
            for(int i=0;i<maxWeaponCountR;i++)
            {
                weaponsR[i]=-1;
            }
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
                var data=SaveSystem.LoadFromJson<SaveData>("WeaponRotationData").Rotations.Find(x=>x.ID==id);
                weapon.DefaultRotationL=data.RotationL;
                weapon.DefaultRotationR=data.RotationR;

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


        }
        [Serializable]
        class SaveData
        {
            public List<WeaponData> Rotations;
            public SaveData()
            {
                Rotations=new List<WeaponData>();
            }
        }

        [Serializable]
        struct WeaponData
        {
            public int ID;
            public Quaternion RotationL;
            public Quaternion RotationR;
        }
        public void Save()
        {
            SaveSystem.SaveByJson("WeaponRotationData",saveData);
        }

        SaveData saveData=new SaveData();

        void AddData()
        {
            saveData.Rotations.Add(new WeaponData()
            {
                ID=currentWeaponLID,
                RotationL=weaponTransL.GetChild(0).transform.localRotation,
                RotationR=weaponTransR.GetChild(0).transform.localRotation
            });
        }


        /// <summary>
        /// 销毁武器实体
        /// </summary>
        private void DestroyWeapon(bool IsLeft)
        {
            if(IsLeft)
            {
                if(weaponTransL.childCount>0)
                {
                    GameObject.Destroy(weaponTransL.GetChild(0).gameObject);
                }
            }
            else
            {
                if(weaponTransR.childCount>0)
                {
                    GameObject.Destroy(weaponTransR.GetChild(0).gameObject);
                }
            }
        }

        /// <summary>
        /// 生成武器实体
        /// </summary>
        /// <param name="IsLeft"></param>
        /// <param name="_WeaponID">武器ID</param>
        private void InitWeapon(bool IsLeft,int _WeaponID)
        {
            if(_WeaponID==-1)
            {
                Debug.Log("没武器");
                return;
            }
            var weaponPre=ResManager.Instance.LoadResource<GameObject>
            ("Item/Weapon","Weapon_"+weaponDic[_WeaponID].id.ToString().PadLeft(4,'0')+".prefab");
            var weapon=GameObject.Instantiate(weaponPre);
            weapon.name=weaponPre.name;
            if(IsLeft)
            {
                weapon.transform.SetParent(weaponTransL.transform);
            }
            else
            {
                weapon.transform.SetParent(weaponTransR.transform);
            }
            weapon.transform.localPosition=Vector3.zero;
            weapon.transform.localRotation=IsLeft?weaponDic[_WeaponID].DefaultRotationL
                                            :weaponDic[_WeaponID].DefaultRotationR;
        }

        /// <summary>
        /// 数据上切换
        /// </summary>
        public void SwitchLWeapon()
        {
            currentWeaponIndexL++;
            if(currentWeaponIndexL>=maxWeaponCountL)
            {
                currentWeaponIndexL=0;
            }
        }

        /// <summary>
        /// 数据上切换
        /// </summary>
        public void SwitchRWeapon()
        {
            currentWeaponIndexR++;
            if(currentWeaponIndexR>=maxWeaponCountR)
            {
                currentWeaponIndexR=0;
            }
        }


       #region  PUBLIC FUNCTION

        /// <summary>
        /// 在指定位置销毁生成指定武器
        /// </summary>
        /// <param name="_index"></param>
        public void RefreshWeapon(bool _IsLeft)
        {
            DestroyWeapon(_IsLeft);

            InitWeapon(_IsLeft,_IsLeft?currentWeaponLID:currentWeaponRID);
        }

        /// <summary>
        /// 数据上进行添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AddWeapon(int ID,HandState handState)
        {
            Debug.Log($"已添加{DataBoard.Instance.BagData.GetBagItemDic()[ID].item.name}");
            switch(handState)
            {
                case HandState.LeftHand:
                   for(int i=0;i<weaponsL.Length;i++)
                   {
                        if(weaponsL[i]==-1)
                        {
                            weaponsL[i]=ID;
                            break;
                        }
                   }
                    break;
                case HandState.RightHand:
                    for(int i=0;i<weaponsR.Length;i++)
                    {
                        if(weaponsR[i]==-1)
                        {
                            weaponsR[i]=ID;
                            break;
                        }
                    }
                    break;
                case HandState.TwoHands:
                    for(int i=0;i<weaponsL.Length;i++)
                    {
                        if(weaponsL[i]==-1)
                        {
                            weaponsL[i]=ID;
                            break;
                        }
                    }
                    for(int i=0;i<weaponsR.Length;i++)
                    {
                        if(weaponsR[i]==-1)
                        {
                            weaponsR[i]=ID;
                            break;
                        }
                    }
                    break;
            }

        }

        /// <summary>
        /// 移除装备栏中的武器（DATA）
        /// </summary>
        /// <param name="ID"></param>
        public void RemoveWeapon(int ID)
        {

            Debug.Log($"已移除{DataBoard.Instance.BagData.GetBagItemDic()[ID].item.name}");

            for(int i=0;i<weaponsL.Length;i++)
            {
                if(weaponsL[i]==ID)
                {
                    weaponsL[i]=-1;
                    break;
                }
            }
            for(int i=0;i<weaponsR.Length;i++)
            {
                if(weaponsR[i]==ID)
                {
                    weaponsR[i]=-1;
                    break;
                }
            }
        }

        /// <summary>
        /// 获取当前武器
        /// </summary>
        /// <param name="IsLeft"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 获取当前武器的攻击次数
        /// </summary>
        /// <param name="IsLeft"></param>
        /// <returns></returns>
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
        /// 获取当前武器字典
        /// </summary>
        /// <returns></returns>
        public Dictionary<int,Weapon> GetWeaponDic()
        {
            return weaponDic;
        }
        /// <summary>
        /// 获取当前武器槽的索引
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int GetCurrentWeaponIndex(bool IsLeft)
        {
            return IsLeft?currentWeaponIndexL:currentWeaponIndexR;
        }

        /// <summary>
        /// 获取最大可装备武器数量
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int GetMaxWeaponCount(bool IsLeft)
        {
            return IsLeft?maxWeaponCountL:maxWeaponCountR;
        }

        /// <summary>
        /// 获取当前武器栏位数组
        /// </summary>
        /// <param name="IsLeft">是否为左手</param>
        /// <returns></returns>
        public int[] GetWeapons(bool IsLeft)
        {
            return IsLeft?weaponsL:weaponsR;
        }

        public void ReSetAttackTimes(bool IsLeft)
        {
            if(IsLeft)
            {
                AttackTimesL=1;
            }
            else
            {
                AttackTimesR=1;
            }

        }
        public void ReSetAttackTimes()
        {
            AttackTimesL=1;
            AttackTimesR=1;
        }
        #endregion

    }
}