using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    [System.Serializable]
    public class BaseAttribute
    {
        //基础移动速度
        readonly float BASE_MOVE_SPEED=300f;
        readonly int BASE_STRENGTH=15;
        readonly int BASE_VIGOR=15;
        readonly int BASE_AGILITY=15;
        readonly int BASE_DEXTERITY=15;
        readonly int BASE_KNOWLEDGE=15;
        readonly int BASE_WILL=15;
        //力量
        int strength;
        public int Strength
        {
            get
            {
                return strength;
            }
            set
            {
                strength = value;
                if (strength < 0)
                {
                    strength = 0;
                }
                else if(strength > 100)
                {
                    strength=100;
                }
                physicalPower=DataCurve.Instance.CalculatePhysicalPower(strength);

            }
        }
        //活力
        int vigor;
        public int Vigor
        {
            get
            {
                return vigor;
            }
            set
            {
                vigor = value;
            }
        }

        //敏捷
        int agility;
        public int Agility
        {
            get
            {
                return agility;
            }
            set
            {
                agility = value;
                MoveSpeed = DataCurve.Instance.CalculateMoveSpeed(agility);
            }
        }
        //灵巧
        int dexterity;
        public int Dexterity
        {
            get
            {
                return dexterity;
            }
            set
            {
                dexterity = value;
            }
        }
        //意志
        int will;
        public int Will
        {
            get
            {
                return will;
            }
            set
            {
                will = value;
                MagicPower=DataCurve.Instance.CalculateMagicPower(will);
                MagicArmorPoint=DataCurve.Instance.CalculateMagicArmorPoint(will);
            }
        }
        //知识
        int knowledge;
        public int Knowledge
        {
            get
            {
                return knowledge;
            }
            set
            {
                knowledge = value;
                MagicSpeed=DataCurve.Instance.CalculateMagicSpeed(knowledge);
                MagicCapacity=DataCurve.Instance.CalculateMagicCapacity(knowledge);
            }
        }

        int physicalArmorPoint;
        public int PhysicalArmorPoint
        {
            get
            {
                return physicalArmorPoint;
            }
            set
            {
                physicalArmorPoint = value;
                PhysicalHitReduction=DataCurve.Instance.CalculatePhysicalHitReduction(physicalArmorPoint);
            }
        }
        int magicArmorPoint;
        public int MagicArmorPoint
        {
            get
            {
                return magicArmorPoint;
            }
            set
            {
                magicArmorPoint = value;
                MagicHitReduction=DataCurve.Instance.CalculateMagicHitReduction(magicArmorPoint);
            }
        }
        //物理力量
        float physicalPower;
        public float PhysicalPower
        {
            get
            {
                return physicalPower;
            }
            set
            {
                physicalPower = value;
                PhysicalPowerBonus=DataCurve.Instance.CalculatePhysicalPowerBonus(physicalPower);
            }
        }
        //魔法力量
        float magicPower;
        public float MagicPower
        {
            get
            {
                return magicPower;
            }
            set
            {
                magicPower = value;
            }
        }





        //----------------------Base----------------------//
        //生命值
        public float Hp=>DataCurve.Instance.CalculateMaxHP(Strength,Vigor)+OtherHp;


        //法力值
        public float Mp;


        //耐力
        public float Stamina;



    /*       //物理治疗
        float physicsHpRecovery;
        public float PhysicsHpRecovery
        {
            get
            {
                return physicsHpRecovery;
            }
            set
            {
                physicsHpRecovery = value;
            }
        }
        //魔法治疗
        float magicHpRecovery;
        public float MagicHpRecovery
        {
            get
            {
                return magicHpRecovery;
            }
            set
            {
                magicHpRecovery = value;
            }
        }
    */


        public int OtherHp;
        public int OtherMp;
        public int OtherStamina;
        //法术容量
        public int MagicCapacity;
        //移动速度
        public float MoveSpeed;
        //动作速度
        public float ActionSpeed;
        //施法速度
        public float MagicSpeed;
        //DeBuff持续时间
        public float DeBuffDuration;
        //Buff 持续时间
        public float BuffDuration;
        //护甲穿透
        public float PhysicalPenetration;
        //魔法穿透
        public float MagicPenetration;
        //爆头伤害减免
        public float HeadHitReduction;
        //飞行物伤害减免
        public float FlyingHitReduction;
        //物理伤害减免
        public float PhysicalHitReduction;
        //魔法伤害减免
        public float MagicHitReduction;
        //物理伤害加成
        public float PhysicalPowerBonus;
        //魔法伤害加成
        public float MagicPowerBonus;
        //武器伤害
        public int WeaponDamage;






    }
    public class PlayerData
    {

        int playerID;
        public int PlayerID
        {
            get
            {
                return playerID;
            }
            set
            {
                playerID = value;
            }
        }

        #region 基础属性
        readonly float BaseStrength=15;
        readonly float BaseVigor=15;
        readonly float BaseAgility=15;
        readonly float BaseDexterity=15;
        readonly float BaseWill=15;
        readonly float BaseKnowledge=15;
        #endregion


        public BaseAttribute BaseAttribute;

        public void Load()
        {

        }
    }
}
