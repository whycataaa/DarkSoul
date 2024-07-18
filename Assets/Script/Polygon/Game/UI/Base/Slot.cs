using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace PolygonProject
{

    //背包格子
    public class Slot : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerMoveHandler
    {
        private int bagItemID=-1;
        public int BagItemID
        {
            get
            {
                return bagItemID;
            }
            set
            {
                if(value!=-1)
                {
                    bagItemID=value;
                    image.sprite=DataBoard.Instance.BagData.GetBagItemDic()[value].item.sprite;
                    Num=DataBoard.Instance.BagData.GetBagItemDic()[value].Num;
                }
                else
                {
                    bagItemID=-1;
                    image.sprite=null;
                }

            }
        }

        Image image;
        TextMeshProUGUI t_Num;
        int num;
        //格子中的数量
        public int Num
        {
            get
            {
                return num;
            }
            set
            {
                num=value;
                t_Num.text=value.ToString();
            }
        }

        bool IsInSlot=false;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            image=GetComponent<Image>();
            t_Num=GetComponentInChildren<TextMeshProUGUI>();
        }
        public void OnPointerEnter(PointerEventData eventData)
        {
            IsInSlot=true;
            PanelManager.Instance.ShowPanel(ShowItemPanel.Instance);
            UIManager.Instance.UIDic[ShowItemPanel.Instance.UIType].transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            if(BagItemID!=-1)
            {
                ShowItemPanel.Instance.SetNameAndInfo(DataBoard.Instance.BagData.GetBagItemDic()[bagItemID].item.name,DataBoard.Instance.BagData.GetBagItemDic()[bagItemID].item.info);
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            IsInSlot=false;
            UIManager.Instance.DisShowUI(ShowItemPanel.Instance.UIType);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            UIManager.Instance.UIDic[ShowItemPanel.Instance.UIType].transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
        }

        /// <summary>
        /// Update is called every frame, if the MonoBehaviour is enabled.
        /// </summary>
        void Update()
        {
            //右键格子
            if(PlayerInputHandler.Instance.IsRightMouse&&IsInSlot)
            {
                ChosePanel.Instance.ChoseItemID=this.bagItemID;
                UIManager.Instance.DisShowUI(ShowItemPanel.Instance.UIType);
                if(PanelManager.Instance.GetPanelStack().Peek()==ChosePanel.Instance)
                {
                    PanelManager.Instance.PanelPop();
                }
                PanelManager.Instance.PanelPush(ChosePanel.Instance);
                UIManager.Instance.UIDic[ChosePanel.Instance.UIType].transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
            }
        }
        public void ShowSlot(bool _isShow)
        {
            gameObject.SetActive(_isShow);
        }
    }
}
