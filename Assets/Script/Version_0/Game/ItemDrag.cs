using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrag : MonoBehaviour
{
    [SerializeField]private GameObject selectedObject;

    [SerializeField]public List<kengData> kengDatas = new List<kengData>();
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //如果没选中物体
            if (selectedObject == null)
            {
                //发射射线
                RaycastHit hit = CastRay();

                //射线击中物体
                if (hit.collider != null)
                {
                    //将物体的标签和CanDrag比对
                    //如果标签不是CanDrag，
                    if (!hit.collider.CompareTag("CanDrag"))
                    {
                        return;
                    }

                    selectedObject = hit.collider.gameObject;
                    Cursor.visible = false;
                }
            }
            //选中物体
            else
            {
                RaycastHit hit = CastRay();
                if (hit.collider != null)
                {
                    //普通放下
                    if (!hit.collider.CompareTag("Keng"))
                    {
/*                        var w = Camera.main.ScreenToWorldPoint(selectedObject.transform.position);
                        Cursor.visible = true;
                        selectedObject.transform.position = new Vector3(w.x, 2f, w.z);*/

                        Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
                        Vector3 p2 = Camera.main.ScreenToWorldPoint(position);
                        selectedObject.transform.position = new Vector3(p2.x, 28.5f, p2.z);
                        selectedObject = null;
                        Cursor.visible = true;
                        Debug.Log("has set");
                        return;
                    }
                    else
                    {
                        Debug.Log("has aaa set");
                        var hitgameobj = hit.collider.gameObject;
                        if (selectedObject.GetComponent<StoneData>().StoneNumber == hitgameobj.GetComponent<kengData>().KengNum)
                        {
                            hitgameobj.GetComponent<kengData>().isTrue = true;
                        }
                        else
                        {
                            hitgameobj.GetComponent<kengData>().isTrue = false;
                        }


                        var pos = hit.collider.gameObject.GetComponent<kengData>().position;
                        //var worldPosition = Camera.main.ScreenToWorldPoint(pos);
                        Cursor.visible = true;
                        selectedObject.transform.position = new Vector3(pos.x, 28.5f, pos.z);
                        selectedObject = null;
                    }
                }

                GAME3CHECK();
            }
        }

        if (selectedObject != null)
        {
            Vector3 position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z);
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(position);
            selectedObject.transform.position = new Vector3(worldPosition.x, 31f, worldPosition.z);

            if (Input.GetMouseButtonDown(1))
            {
                selectedObject.transform.rotation = Quaternion.Euler(new Vector3(
                    selectedObject.transform.rotation.eulerAngles.x,
                    selectedObject.transform.rotation.eulerAngles.y + 90f,
                    selectedObject.transform.rotation.eulerAngles.z));
            }
        }
    }
    /// <summary>
    /// 杨辉三角游戏检测
    /// </summary>
    /// <exception cref="NotImplementedException"></exception>
    private void GAME3CHECK()
    {
        foreach(var a in kengDatas)
        {
            //如果有不对的
            if(!a.isTrue)
            {
                Debug.Log("Has Error"+a.name);
                return;
            }


        }
            Scene2CameraSwitch.Instance.SwitchTOPlayerCamera();
    }

    private RaycastHit CastRay()
    {

        Vector3 screenMousePosFar = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 screenMousePosNear = new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.nearClipPlane);
        Vector3 worldMousePosFar = Camera.main.ScreenToWorldPoint(screenMousePosFar);
        Vector3 worldMousePosNear = Camera.main.ScreenToWorldPoint(screenMousePosNear);
        RaycastHit hit;
        if(selectedObject!=null)
        {
           Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit,1000,~(1<<12));
    
        }else
        {
            Physics.Raycast(worldMousePosNear, worldMousePosFar - worldMousePosNear, out hit);
        }

        return hit;
    }
}
