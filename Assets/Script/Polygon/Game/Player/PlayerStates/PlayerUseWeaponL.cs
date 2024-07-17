using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    /// <summary>
    /// 使用左手武器的状态
    /// </summary>
    [CreateAssetMenu(menuName ="Data/Polygon/PlayerUseWeaponL",fileName ="UseWeaponL_Player")]
    public class PlayerUseWeaponL : PlayerState
    {
        public override void Enter()
        {
            GetStateStartTime();
            playerControl.SetZeroVelocity();
            AttackAnim();
            playerControl.playerWeaponManager.AttackTimesL++;
            if(playerControl.playerWeaponManager.AttackTimesL>playerControl.playerWeaponManager.GetCurrentWeaponMaxAttackTimes(true))
            {
                playerControl.playerWeaponManager.AttackTimesL=1;
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

            Weapon currentWeapon=playerControl.playerWeaponManager.GetCurrentWeapon(true);
            if(currentWeapon!=null)
            {
                Debug.Log(currentWeapon.id+"AttackL"+playerControl.playerWeaponManager.AttackTimesL);
                animator.CrossFade(currentWeapon.id+"AttackL"+playerControl.playerWeaponManager.AttackTimesL,0.1f);
            }
            else
            {
                animator.CrossFade("Hand"+playerControl.playerWeaponManager.AttackTimesL,0.1f);
                Debug.Log("未装备武器");
            }

        }
    }
}
