using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using UnityEngine;
namespace game2{
/// <summary>
/// 轻型武器攻击状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Attack/DarkSwordAttack_Player",fileName ="DarkSwordAttack_Player")]
public class PlayerLightSwordAttackState : PlayerAttackState
{

    //攻击时向前的力
    public float[] force;
    //多端攻击动画的名字
    public string[] stateNames;
    public int[] stateHashs;
    int currentAttackIndex=0;
    //检测攻击输入
    bool attackInput=false;
    //四段攻击的体力消耗
    public float[] Stamina=new float[4];

    public override void OnEnable()
    {
        currentAttackIndex=0;
        stateHashs=new int[stateNames.Length];
        //多个动画时遍历获取哈希值
        int index=0;
        foreach(var stateName in stateNames)
        {
            stateHashs[index]=Animator.StringToHash(stateName);
            index++;
        }

    }
    public override void Enter()
    {
        //订阅受击事件
        PlayerStateMachine.OnAttackReceived+=HandleAttackReceived;
        playerController.CanRecoverStamina=false;
        GetStateStartTime();
        //设置速度为0
        SetZeroVelocity();
        //朝向最近敌人
        playerController.nearestEnemyTrans=playerController.GetNearestEnemyPosition();
        playerController.RotateToEnemy();
        //如果超出了连击时间，那么重置连击
        if(playerController.ComboTime<=0)
        {
            currentAttackIndex=0;
        }

        //攻击位移
        playerController.rb.AddForce(playerController.player.transform.forward*force[currentAttackIndex],ForceMode.Impulse);
        //播放动画
        animator.Play(stateHashs[currentAttackIndex]);
        //消耗体力
        playerController.CurrentStamina-=Stamina[currentAttackIndex];
        currentAttackIndex++;
        if(currentAttackIndex>=4)
        currentAttackIndex=0;
    }


    public override void LogicUpdate()
    {

        if(playerController.CanCombo)
        {
            if(PlayerInput.Instance.IsAttack)
            {
                attackInput=true;
            }
        }
        if(attackInput&&playerController.CanSwitch)
        {
            playerStateMachine.ChangeState(typeof(PlayerLightSwordAttackState));
        }
        if(PlayerInput.Instance.MoveVector2!=Vector2.zero&&playerController.CanSwitch)
        {
            playerStateMachine.ChangeState(typeof(PlayerRunStart));
        }
        //动画结束
        if(IsAnimationFinished)
        {

            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }

    }

    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {
        base.Exit();
        attackInput=false;
        playerController.ComboTime=playerController.COMBO_TIME;
    }

    public override void HandleAttackReceived(AttackInfo info)
    {

    }

}
}