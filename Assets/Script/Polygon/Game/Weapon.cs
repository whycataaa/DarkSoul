using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class Weapon:Item
    {
        //左手攻击段数
        public int LeftHandAttackTimes;
        //右手攻击段数
        public int RightHandAttackTimes;
        //双手攻击段数
        public int TwoHandsAttackTimes;
        //武器攻击力
        public float Attack;
        //武器防御力
        public float Defense;
        public Quaternion DefaultRotationL;
        public Quaternion DefaultRotationR;
        //动画名称格式为：武器ID+Attack+L/R/T+段数
        public Weapon(int _id,float _Attack,float _Defense,int _LeftHandAttackTimes,int _RightHandAttackTimes,int _TwoHandsAttackTimes)
        {
            id=_id;
            Attack=_Attack;
            Defense=_Defense;
            LeftHandAttackTimes=_LeftHandAttackTimes;
            RightHandAttackTimes=_RightHandAttackTimes;
            TwoHandsAttackTimes=_TwoHandsAttackTimes;
        }

    }


}
