using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public GameObject info;
    public Button baggridBT;
    public BagGrid bagGrid;
    public BagItemShow itemShow;
    private void Awake()
    {
        bagGrid=GetComponent<BagGrid>();
        itemShow = GetComponentInParent<BagItemShow>();
    }
    protected void Start()
    {
        baggridBT.onClick.AddListener(()=>ButtonaClick());

    }
    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    void OnEnable()
    {
        info.SetActive(false);
    }





    public void ButtonaClick()
    {
        itemShow.itemPanel.SetActive(false);
        info.SetActive(!info.activeSelf);
        Debug.Log("Button Right Click");
    }
    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    void OnDisable()
    {
        info.SetActive(false);
    }
}


