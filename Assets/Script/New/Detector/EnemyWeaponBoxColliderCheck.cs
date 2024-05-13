using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponBoxColliderCheck : MonoBehaviour
{
    PlayerControl playerControl;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        playerControl=GameObject.Find("PlayerControl").GetComponent<PlayerControl>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            Debug.Log(other.name);
            playerControl.IsHit=true;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.tag=="Player")
        {
            playerControl.IsHit=false;
        }
    }
}
