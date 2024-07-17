using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    //1.持有所有状态类，对他们进行管理和切换

    //2.进行当前状态的更新
    protected IState currentState;
    //使用字典管理状态
    protected Dictionary<System.Type,IState> stateTable;
    public void LogicUpdate()
    {
        currentState?.LogicUpdate();
    }
    public void PhysicUpdate()
    {
        currentState?.PhysicUpdate();
    }
    //进入状态
    public void SwitchOn(IState newState)
    {
        currentState=newState;
        currentState.Enter();
    }
    //改变状态
    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        SwitchOn(newState);
    }
    //改变状态使用数据字典的方法重载
    public void ChangeState(System.Type newStateType)
    {
        ChangeState(stateTable[newStateType]);
    }

}