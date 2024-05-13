using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityTransform;
using UnityEngine;
/// <summary>
/// 轻型武器攻击状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Attack/DarkSwordAttack_Player",fileName ="DarkSwordAttack_Player")]
public class PlayerLightSwordAttackState : PlayerAttackState
{
    //多端攻击动画的名字
    public string[] stateNames;
    public int[] stateHashs;
    int currentAttackIndex=0;
    //检测攻击输入
    bool attackInput=false;
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
        //设置速度为0
        SetZeroVelocity();
        GetStateStartTime();
        playerController.nearestEnemyTrans=playerController.GetNearestEnemyPosition();


        Debug.Log("enter Attack");

        animator.Play(stateHashs[currentAttackIndex]);
        currentAttackIndex++;
        if(currentAttackIndex>=4)
        currentAttackIndex=0;
    }


    public override void LogicUpdate()
    {
        playerController.RotateToEnemy();
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
        //动画结束
        if(IsAnimationFinished)
        {
            Debug.Log("finish state");
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }
        // Debug.Log(stateDuration);
        // Debug.Log(animator.GetCurrentAnimatorStateInfo(0).length);
        // Debug.Log(IsAnimationFinished);
    }

    public override void PhysicUpdate()
    {

    }
    public override void Exit()
    {
        attackInput=false;
    }



}
