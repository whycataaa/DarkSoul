
//状态接口
public interface IState
{
    /// <summary>
    /// 进入状态调用一次
    /// </summary>
    void Enter();
    /// <summary>
    /// 退出状态调用一次
    /// </summary>
    void Exit();
    /// <summary>
    /// Update
    /// </summary>
    void LogicUpdate();
    /// <summary>
    /// FixedUpdate
    /// </summary>
    void PhysicUpdate();
}
