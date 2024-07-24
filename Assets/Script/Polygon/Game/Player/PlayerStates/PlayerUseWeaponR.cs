using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace PolygonProject
{
    /// <summary>
    /// 使用右手武器的状态
    /// </summary>
    [CreateAssetMenu(menuName ="Data/Polygon/PlayerUseWeaponR",fileName ="UseWeaponR_Player")]
    public class PlayerUseWeaponR : PlayerState
    {

        public override void Enter()
        {
            GetStateStartTime();
            playerControl.SetZeroVelocity();
            AttackAnim();
            playerControl.playerWeaponManager.AttackTimesR++;
            if(playerControl.playerWeaponManager.AttackTimesR>playerControl.playerWeaponManager.GetCurrentWeaponMaxAttackTimes(false))
            {
                playerControl.playerWeaponManager.AttackTimesR=1;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            if(IsAnimationFinished)
            {
                playerStateMachine.ChangeState(typeof(PlayerIdle));
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();
        }

        public override void Exit()
        {
            base.Exit();

        }
        private void AttackAnim()
        {
            Weapon currentWeapon=playerControl.playerWeaponManager.GetCurrentWeapon(false);

            if(currentWeapon!=null)
            {
                Debug.Log(currentWeapon.ID+"AttackR"+playerControl.playerWeaponManager.AttackTimesR);
                animator.CrossFade(currentWeapon.ID+"AttackR"+playerControl.playerWeaponManager.AttackTimesR,0.1f);
            }
            else
            {
                animator.CrossFade("Hand"+playerControl.playerWeaponManager.AttackTimesR,0.1f);
                Debug.Log("未装备武器");
            }

        }
    }
}
