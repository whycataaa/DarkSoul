using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class closeBook : MonoBehaviour
{
    public Button closeBoook_Bt;

    private void Awake()
    {
        closeBoook_Bt.onClick.AddListener(()=>CLOSEBook());
    }

    private void CLOSEBook()
    {
        closeBoook_Bt.gameObject.SetActive(false);
        CameraControl.Instance.END2DScene();
    }
}
