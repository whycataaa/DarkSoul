using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagItemShow : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public GameObject itemPanel;

    public void OnPointerEnter(PointerEventData eventData)
    {   
        //背包有东西
        if(this.gameObject.transform.childCount!=0)
        {
            itemPanel.transform.position= eventData.position;
            itemPanel.SetActive(true);
            itemPanel.GetComponentInChildren<Text>().text = this.GetComponentInChildren<BagGrid>().ItemName;
        }

        }
    public void OnPointerExit(PointerEventData eventData)
    {
        itemPanel.SetActive(false);
    }

    private void Awake()
    {
        itemPanel.SetActive(false);
    }
}
