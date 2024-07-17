using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameaSwitch : MonoBehaviour
{


    public CinemachineBrain cinemachineBrain;
    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineVirtualCamera VirtualCamera2;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {

            VirtualCamera.gameObject.SetActive(false);
            VirtualCamera2.gameObject.SetActive(true);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            VirtualCamera2.gameObject.SetActive(false);
            VirtualCamera.gameObject.SetActive(true);
        }
    }
}
