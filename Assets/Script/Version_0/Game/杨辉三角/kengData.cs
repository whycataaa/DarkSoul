using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kengData : MonoBehaviour
{
    public Vector3 position;
    public int KengNum;

    public bool isTrue = false;
    private void Awake()
    {
        position = transform.position;
    }
}
