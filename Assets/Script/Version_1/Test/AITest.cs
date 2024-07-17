using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITest : MonoBehaviour
{

    private NavMeshAgent agent;
    Vector3 directionToPlayer;
    public GameObject player;
    public Rigidbody rb;
    public float targetDistance;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        agent=GetComponent<NavMeshAgent>();
    }
    public void Update()
    {
        //朝向玩家
        var pos=player.transform.position;
        pos.y=transform.position.y;
        rb.transform.LookAt(pos);
        directionToPlayer = player.transform.position - transform.position;
        Debug.Log(directionToPlayer.magnitude > targetDistance);

        // 如果敌人和玩家之间的距离大于停止距离，移动敌人
        if (directionToPlayer.magnitude > targetDistance)
        {
            agent.SetDestination(player.transform.position);

        }
        else
        {
            // 如果敌人和玩家之间的距离小于等于停止距离，停止移动
            rb.velocity = Vector3.zero;
        }
    }
}
