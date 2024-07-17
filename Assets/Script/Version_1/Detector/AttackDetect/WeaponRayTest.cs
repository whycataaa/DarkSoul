using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 近战攻击射线检测
/// </summary>
public class WeaponRayTest : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public LayerMask layer;
    int hitCount;
    public Transform[] Points; //射线发射点
    public Dictionary<int, Vector3> dic_lastPoints = new Dictionary<int, Vector3>(); //存放上个位置信息
    public GameObject particle;//粒子效果
    public PlayerControl playerController;

    //可攻击怪物的字典
    public Dictionary<string,EnemyControl> enemyTable;
    public GameObject beHitObj;
    //无敌时间
    public float wuDiTime;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerController=GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
    }
    private void Start()
    {
        if (dic_lastPoints.Count == 0)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                dic_lastPoints.Add(Points[i].GetHashCode(), Points[i].position);
            }
        }
    }
    private void LateUpdate()
    {
        var newA = pointA.position;
        var newB = pointB.position;
        Debug.DrawLine(newA, newB, Color.red, 1f);
        // if(playerController.CanDamage)
        // {
        //     SetPosition(Points);
        // }

    }


    void SetPosition(Transform[] points)
    {
        for (int i = 0; i < points.Length; i++)
        {
            var nowPos = points[i];
            dic_lastPoints.TryGetValue(nowPos.GetHashCode(), out Vector3 lastPos);
            //Debug.DrawLine(nowPos.position, lastPos, Color.blue, 1f); ;
            Debug.DrawRay(lastPos,   nowPos.position- lastPos, Color.blue, 1f);
           
            Ray ray = new Ray(lastPos, nowPos.position - lastPos);
            RaycastHit[] raycastHits = new RaycastHit[8];
            Physics.RaycastNonAlloc(ray, raycastHits, Vector3.Distance(lastPos, nowPos.position), layer, QueryTriggerInteraction.Ignore);
 
            foreach (var item in raycastHits)
            {
                if (item.collider==null) continue;

                if(enemyTable.ContainsKey(item.collider.name))
                {
                    Debug.Log(item.collider.name);
                  //  enemyTable[item.collider.name].IsHit=true;
                    enemyTable[item.collider.name].currentHp-=10;
                    Debug.Log(enemyTable[item.collider.name].currentHp);
                }

                //注意:在同一帧会多次击中一个对象
                StartCoroutine(StopHit());
                if (particle)
                {
                    var go = Instantiate( particle, item.point,Quaternion.identity);
                    Destroy(go, 3f);
                }
                hitCount++;
                break;
            }
  
            if (nowPos.position != lastPos)
            { 
                dic_lastPoints[nowPos.GetHashCode()] = nowPos.position;//存入上个位置信息
            } 
        } 
    } 
 

    /// <summary>
    /// 预先设定一个触发器碰撞体，当怪物进入时将怪物信息存入列表供攻击检测时使用
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Enemy")
        {
            var enemyControl=other.GetComponent<EnemyControl>();
            enemyTable.Add(other.name,enemyControl);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag=="Enemy")
        {
            enemyTable.Remove(other.name);
        }
    }
    private void OnGUI()
    { 
        var labelstyle = new GUIStyle();
        labelstyle.fontSize = 32;
        labelstyle.normal.textColor = Color.white;
        int height = 40; 
        GUIContent[] contents = new GUIContent[]
        {
               new GUIContent($"hitCount:{hitCount}"),
               new GUIContent($"frameCount:{Time.frameCount }"),
         };
 
        for (int i = 0; i < contents.Length; i++)
        {
            GUI.Label(new Rect(0, height * i, 180, 80), contents[i], labelstyle);
        }
    } 


    IEnumerator StopHit()
    {
        yield return new WaitForSeconds(wuDiTime);
        //beHitObj.transform.parent.GetComponent<EnemyControl>().IsHit=false;
    }
}

}