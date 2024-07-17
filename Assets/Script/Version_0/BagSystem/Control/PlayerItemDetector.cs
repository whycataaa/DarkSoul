using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItemDetector : MonoBehaviour
{

    ItemManager itemManager;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        itemManager=FindObjectOfType(typeof(ItemManager)) as ItemManager;
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main != null)
        {
            //创建一条从主摄像机到鼠标触摸点的射线(控制物体被选中时的高亮)
            Ray ray1 = Camera.main.ScreenPointToRay(Input.mousePosition);



            RaycastHit hitInfo = new RaycastHit();
            if (Physics.Raycast(ray1, out hitInfo))
            {
                var itemObj = hitInfo.collider.gameObject;

                if (itemObj.tag == "Item")
                {
                    //物体高亮
                    Debug.Log("Item was selected");

                    if (Input.GetKeyDown(KeyCode.F))
                    {


                        TaskManager.Instance.UpdateTaskData(itemObj.name, 1);
                        var item = itemObj.GetComponent<SceneItem>().item;
                        itemManager.AddItem(item);
                        Destroy(itemObj);

                    }

                }
                if (itemObj.tag == "TWYQ")
                {
                        Debug.Log("is SXYQ");
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                         BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable[itemObj.name]);
                                toastUI.Instance.Showtoast(itemObj.name + "已添加至笔记");
                    }
       

                }
            }
        }
    }
}
