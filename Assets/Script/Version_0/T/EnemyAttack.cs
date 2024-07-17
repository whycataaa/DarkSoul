using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{

    // Start is called before the first frame update
    public int Attack = 20;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Player")
        {
            Debug.Log("player is attacked");
            StarterAssets.ThirdPersonControllerCopy tpc = other.GetComponent<StarterAssets.ThirdPersonControllerCopy>();
            if (tpc != null)
            {
                tpc.HP -= 20;
            }
        }
    }

}
