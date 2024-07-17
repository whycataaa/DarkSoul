using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AgentHitBox : MonoBehaviour
{
    //受击主体
    public IAgent target;

    public GameObject agent;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        target=agent.GetComponentInParent<IAgent>();

    }
    public void GetDamage(AttackInfo info)
    {
        target.GetDamage(info);
    }
}
