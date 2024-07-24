using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public enum EDamageType
    {
        Fire,
        Ice,
        Electric,
        Holy,
        Shadow,
        Arithmetic
    }
    public enum EQuality
    {
        //普通
        Common,
        //稀有
        Rare,
        //史诗
        Epic,
        //传说
        Legendary
    }
    public class Weapon:Item
    {
        //武器基础攻击力
        public float BaseDamage;
        //武器移动速度
        public float MoveSpeed;
        //攻击倍率
        public List<int> AttackMultiplier;
        //攻击段数
        public int AttackTimes=>AttackMultiplier.Count;
        //冲击力
        public int ImpactPower;
        //命中减速
        public int HitMoveSpeed;
        //命中减速时间
        public int HitMoveSpeedTime;
        //火
        public int BaseFireDamage;
        //冰
        public int BaseIceDamage;
        //电
        public int BaseElectricDamage;
        //神圣
        public int BaseHolyDamage;
        //暗影
        public int BaseShadowDamage;
        //奥数
        public int BaseArithmeticDamage;
        //初始旋转
        public Quaternion DefaultRotationL;
        public Quaternion DefaultRotationR;


        //动画名称格式为：武器ID+Attack+L/R/T+段数
        public Weapon(
                        int _ID,
                        string _Name,
                        string _Info,
                        int _MaxStackCount,
                        float _BaseDamage,
                        float _MoveSpeed,
                        List<int> _AttackMultiplier,
                        int _ImpactPower,
                        int _HitMoveSpeed,
                        int _HitMoveSpeedTime,
                        EQuality _Quality,
                        string _IconID,
                        int _BaseFireDamage,
                        int _BaseIceDamage,
                        int _BaseElectricDamage,
                        int _BaseHolyDamage,
                        int _BaseShadowDamage,
                        int _BaseArithmeticDamage
                     )
        {
            ItemType=ItemType.Weapon;
            ID=_ID;
            Name=_Name;
            Info=_Info;
            MaxStackCount=_MaxStackCount;
            BaseDamage=_BaseDamage;
            MoveSpeed=_MoveSpeed;
            AttackMultiplier=_AttackMultiplier;
            ImpactPower=_ImpactPower;
            HitMoveSpeed=_HitMoveSpeed;
            HitMoveSpeedTime=_HitMoveSpeedTime;
            Quality=_Quality;
            IconID=_IconID;
            BaseFireDamage=_BaseFireDamage;
            BaseIceDamage=_BaseIceDamage;
            BaseElectricDamage=_BaseElectricDamage;
            BaseHolyDamage=_BaseHolyDamage;
            BaseShadowDamage=_BaseShadowDamage;
            BaseArithmeticDamage=_BaseArithmeticDamage;
        }

    }


}
