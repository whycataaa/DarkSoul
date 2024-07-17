using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicRay : MonoBehaviour
{

    public float MagicRayDamage;
    AttackInfo magicRay;
    //攻击检测的列表
    public List<Detection> detections=new List<Detection>();
    //销毁计时
    public float time=2.5f;
    [SerializeField]public bool OnDetect=false;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        magicRay=new AttackInfo(MagicRayDamage,WeaponType.Default);
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (time >= 0)
            time -= Time.deltaTime;
        else
            Destroy(gameObject);

        if(time<=2.15f)
        {
            OnDetect=true;
        }
        ToggleDetection(OnDetect);
    }

    /// <summary>
    /// 执行攻击检测
    /// </summary>
    void AttackDetection()
    {
        if(OnDetect)
        {
            foreach(var detection in detections)
            {
                foreach(var hit in detection.GetDetection())
                {
                    hit.GetComponent<AgentHitBox>().GetDamage(magicRay);
                }
            }
        }
    }

    public void ToggleDetection(bool value)
    {
        if(value)
        {
            AttackDetection();
        }
        else
        {
            foreach(var detection in detections)
            {
                detection.ClearWasHit();
            }
        }
    }

  
}
