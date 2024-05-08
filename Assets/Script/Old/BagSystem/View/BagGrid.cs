using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using StarterAssets;
/// <summary>
/// 每个背包格子
/// </summary>
public class BagGrid : MonoBehaviour
{
    public ThirdPersonControllerCopy player;
    public Image gridImage;
    public Text gridNum;
    public string ItemName;

    public Text itemName;


    public Button Bt_UseItem;
    public Button Bt_Remove;
    private void Awake()
    {
        player = GameObject.FindObjectOfType(typeof(ThirdPersonControllerCopy)) as ThirdPersonControllerCopy;
        var canvas = GameObject.Find("Canvas_Main");
        Bt_UseItem.onClick.AddListener(UseItem);


    }

    private void UseItem()
    {

        var item = ItemManager.Instance.FindItem(ItemName.ToString());

        if (item == null) { return; }
        if (item.itemType == Item_SO.ItemType.book)
        {
            Debug.Log(item.name == "主角的笔记");
            if (item.name == "主角的笔记")
            {
                Debug.Log(CameraControl.Instance);
                Panel_BagView.Instance.panel_Bag.SetActive(false);
                CameraControl.Instance.Start2DScene();
            } 
            else
            { 
                if (item.itemName == "《乘除通变本末》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["乘除通变本末"]);

                } else if (item.itemName == "《日用算法》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["日用算法"]);
                } else if (item.itemName == "《九章算术注》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["九章算术注"]);
                } else if (item.itemName == "《黄帝九章算经细草》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["黄帝九章算经细草"]);
                } else if (item.itemName == "《周髀算经》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["周髀算经"]);
                } else if (item.itemName == "《详解九章算法上》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["详解九章算法上"]);
                }
                else if (item.itemName == "《详解九章算法中》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["详解九章算法中"]);
                }
                else if (item.itemName == "《详解九章算法下》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["详解九章算法下"]);
                } else if (item.itemName == "《续古摘奇算法》")
                {
                    BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["续古摘奇算法"]);
                }
                toastUI.Instance.Showtoast("笔记已更新");
                ItemManager.Instance.DeleteItem(item);
            }



        }


        else if (item.itemType == Item_SO.ItemType.prop)
        {
            if (item.name == "气血散")
            {
                if (player.HP <= 80)
                {
                    player.HP += 20;
                }
                else
                {
                    player.HP = 100;
                }
                UIManager.Instance.UpdateHPBar();
                ItemManager.Instance.DeleteItem(item);
            }
        }

    }


}