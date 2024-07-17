using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UIElements;
namespace game2{
/// <summary>
/// 当怪物进入一定范围存储其数据，避免检测时获取组件
/// </summary>
public class EnemyDetector : MonoBehaviour
{

    public BoxCollider boxCollider;
    public GameObject player;
    //可攻击怪物的字典
    public Dictionary<string,EnemyControl> enemyTable;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {

        boxCollider=GetComponent<BoxCollider>();
        enemyTable=new Dictionary<string, EnemyControl>();
    }
    private void Start() {
        player=GameObject.Find("PlayerControl/Player");
    }
    void Update()
    {
        this.transform.position=player.transform.position;
    }

    /// <summary>
    /// 预先设定一个触发器碰撞体，当怪物进入时将怪物信息存入列表供攻击检测时使用
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="EnemyBody")
        {
            var enemyControl=other.GetComponentInParent<EnemyControl>();
            enemyTable.Add(other.name,enemyControl);
            Debug.Log("add "+other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="EnemyBody")
        {
            enemyTable.Remove(other.name);
            Debug.Log("remove "+other.name);
        }
    }


}
}