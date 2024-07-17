using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 攻击检测的抽象基类
/// </summary>
public abstract class Detection : MonoBehaviour
{
    public string[] targetTags;
    public List<GameObject> wasHit=new List<GameObject>();

    public abstract List<Collider> GetDetection();
    public void ClearWasHit()=>wasHit.Clear();
}
