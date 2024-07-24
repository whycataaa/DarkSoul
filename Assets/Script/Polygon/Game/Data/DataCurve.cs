using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class DataCurve : SingletonMono<DataCurve>
    {
        [Header("力量-物理力量")]
        public AnimationCurve Strength_PhysicalPowerCurve;
        [Header("物理力量-物理伤害加成")]
        public AnimationCurve PhysicalPower_PhysicalPowerBonusCurve;
        [Header("敏捷-移动速度")]
        public AnimationCurve Agility_MoveSpeedCurve;
        [Header("意志-魔法力量")]
        public AnimationCurve Will_MagicPowerCurve;
        [Header("魔法力量-魔法伤害加成")]
        public AnimationCurve MagicPower_MagicPowerBonusCurve;
        [Header("意志-魔法抗性等级")]
        public AnimationCurve Will_MagicArmorPointCurve;
        [Header("魔法抗性等级-魔法伤害减免")]
        public AnimationCurve MagicArmorPoint_MagicHitReduceCurve;
        [Header("护甲等级-物理伤害减免")]
        public AnimationCurve PhysicalArmorPoint_PhysicalHitReduceCurve;
        [Header("知识-施法速度")]
        public AnimationCurve Knowledge_MagicSpeedCurve;
        [Header("知识-法术容量")]
        public AnimationCurve Knowledge_MagicCapacityCurve;
        [Header("力量*0.25+活力*0.75-最大血量")]
        public AnimationCurve StrengthAndVigor_MaxHPCurve;
        [Header("敏捷*0.25+灵巧*0.75-行动速度")]
        public AnimationCurve AgilityAndDexterity_ActionSpeedCurve;





        #region 数值计算方法
        /// <summary>
        /// 计算物理力量
        /// </summary>
        /// <param name="_Strength">力量</param>
        /// <returns>物理力量</returns>
        public float CalculatePhysicalPower(int _Strength)
        {
            return Strength_PhysicalPowerCurve.Evaluate(_Strength);
        }

        /// <summary>
        /// 计算物理伤害加成
        /// </summary>
        /// <param name="_PhysicalPower">物理力量</param>
        /// <returns>物理伤害加成</returns>
        public float CalculatePhysicalPowerBonus(float _PhysicalPower)
        {
            return PhysicalPower_PhysicalPowerBonusCurve.Evaluate(_PhysicalPower);
        }

        /// <summary>
        /// 计算移动速度
        /// </summary>
        /// <param name="_Agility">敏捷</param>
        /// <returns>移动速度</returns>
        public float CalculateMoveSpeed(int _Agility)
        {
            return Agility_MoveSpeedCurve.Evaluate(_Agility);
        }

        /// <summary>
        /// 计算魔法力量
        /// </summary>
        /// <param name="_Will">意志</param>
        /// <returns>魔法力量</returns>
        public float CalculateMagicPower(int _Will)
        {
            return Will_MagicPowerCurve.Evaluate(_Will);
        }

        /// <summary>
        /// 计算魔法伤害加成
        /// </summary>
        /// <param name="_MagicPower">魔法力量</param>
        /// <returns>魔法伤害加成</returns>
        public float CalculateMagicPowerBonus(float _MagicPower)
        {
            return MagicPower_MagicPowerBonusCurve.Evaluate(_MagicPower);
        }

        /// <summary>
        /// 计算魔法抗性等级
        /// </summary>
        /// <param name="_Will">意志</param>
        /// <returns>魔法抗性等级</returns>
        public int CalculateMagicArmorPoint(int _Will)
        {
            return (int)Will_MagicArmorPointCurve.Evaluate(_Will);
        }

        /// <summary>
        /// 计算魔法减伤率
        /// </summary>
        /// <param name="_MagicArmorPoint">魔法抗性等级</param>
        /// <returns>魔法减伤率</returns>
        public float CalculateMagicHitReduction(int _MagicArmorPoint)
        {
            return MagicArmorPoint_MagicHitReduceCurve.Evaluate(_MagicArmorPoint);
        }

        /// <summary>
        /// 计算物理减伤率
        /// </summary>
        /// <param name="_PhysicalArmorPoint">物理抗性等级</param>
        /// <returns>物理减伤率</returns>
        public float CalculatePhysicalHitReduction(int _PhysicalArmorPoint)
        {
            return PhysicalArmorPoint_PhysicalHitReduceCurve.Evaluate(_PhysicalArmorPoint);
        }

        /// <summary>
        /// 计算施法速度
        /// </summary>
        /// <param name="_Knowledge">知识</param>
        /// <returns>施法速度</returns>
        public float CalculateMagicSpeed(int _Knowledge)
        {
            return Knowledge_MagicSpeedCurve.Evaluate(_Knowledge);
        }

        /// <summary>
        /// 计算法术容量
        /// </summary>
        /// <param name="_Knowledge">知识</param>
        /// <returns>法术容量</returns>
        public int CalculateMagicCapacity(int _Knowledge)
        {
            return (int)Knowledge_MagicCapacityCurve.Evaluate(_Knowledge);
        }

        /// <summary>
        /// 计算最大血量
        /// </summary>
        /// <param name="_Strength">力量</param>
        /// <param name="_Vigor">活力</param>
        /// <returns>最大血量</returns>
        public float CalculateMaxHP(int _Strength,int _Vigor)
        {
            return StrengthAndVigor_MaxHPCurve.Evaluate(_Strength*0.25f+_Vigor*0.75f);
        }
        /// <summary>
        /// 计算动作速度
        /// </summary>
        /// <param name="_Agility">敏捷</param>
        /// <param name="_Dexterity">活力</param>
        /// <returns>动作速度</returns>
        public float CalculateActionSpeed(int _Agility,int _Dexterity)
        {
            return AgilityAndDexterity_ActionSpeedCurve.Evaluate(_Agility*0.25f+_Dexterity*0.75f);
        }
    
        #endregion





    }
}
