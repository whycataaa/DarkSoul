using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 地面检测
/// </summary>
public class GroundDetector : MonoBehaviour
{
    [Header("检测半径")]
    [SerializeField]float detectionRadius;
    [SerializeField]LayerMask groundLayer;
    Collider[] colliders =new Collider[1];
    [SerializeField]public bool IsGround=>Physics.OverlapSphereNonAlloc(transform.position,detectionRadius,colliders,groundLayer)!=0;


    void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }


}
