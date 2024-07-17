using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//受击主体继承该接口
public interface IAgent
{
    void GetDamage(AttackInfo info);
}
