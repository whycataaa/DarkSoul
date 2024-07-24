using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    public class TestButton : MonoBehaviour
    {


        public Button button;
        public Button button2;
        public Button button3;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            button.onClick.AddListener(Isdown);
            button2.onClick.AddListener(Save);
            button3.onClick.AddListener(Load);
        }

        private void Load()
        {
            DataBoard.Instance.Load();
        }

        void Save()
        {
            DataBoard.Instance.Save();
        }
        void Isdown()
        {
            var item=ItemManager.Instance.InitItem(1,EQuality.Epic);
            DataBoard.Instance.BagItemDic.Add(1,item);
            Debug.Log(item.item.Name);
            EventTriggerExt.TriggerEvent(this,EventName.AddItem,new ItemEventArgs{BagItemID=1,ItemNum=0});
        }

    }
}
