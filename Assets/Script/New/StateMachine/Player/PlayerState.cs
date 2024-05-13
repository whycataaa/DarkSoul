using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using UnityEngine;



public class PlayerState : ScriptableObject, IState
{
    //实现动画的切换
    protected Animator animator;
    //实现状态的切换
    protected PlayerStateMachine playerStateMachine;
    //玩家控制器
    protected PlayerControl playerController;
    //状态名和状态名的哈希值，用来做淡入淡出
    [SerializeField]protected string stateName;
    protected int stateHash;
    //状态融合的时间
    [SerializeField,Range(0f,1f)]protected float transDuration=0.05f;
    protected float currentSpeed;
    protected float stateStartTime;
    protected float stateDuration=>Time.time-stateStartTime;
    protected bool IsAnimationFinished=>stateDuration>=animator.GetCurrentAnimatorStateInfo(0).length;

    /// <summary>
    /// 动画的初始化，根据stateName获取哈希值
    /// </summary>
    public virtual void OnEnable()
    {
        stateHash=Animator.StringToHash(stateName);
    }
    //初始化方法
    public void Initialize(Animator _animator,PlayerStateMachine _playerStateMachine,PlayerControl _playerController)
    {
        this.animator=_animator;
        this.playerController=_playerController;
        this.playerStateMachine=_playerStateMachine;
    }
    public virtual void Enter()
    {
        animator.CrossFade(stateHash,transDuration);
        Debug.Log("Enter: "+GetType().ToString());
        GetStateStartTime();
    }



    public virtual void HandleInput()
    {
        
    }

    public virtual void Exit()
    {
        
    }

    public virtual void LogicUpdate()
    {
        
    }

    public virtual void PhysicUpdate()
    {
        
    }
    
    public void GetStateStartTime()
    {
        stateStartTime=Time.time;
    }
        /// <summary>
        /// 将设置玩家的速度为0
        /// </summary>
    protected void SetZeroVelocity()
    {
        playerController.rb.velocity=Vector3.zero;
    }
}

