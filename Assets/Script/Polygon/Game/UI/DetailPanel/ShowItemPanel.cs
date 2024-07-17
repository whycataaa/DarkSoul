using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    public class ShowItemPanel : BasePanel
    {
        #region 单例
        static ShowItemPanel instance;
        public static ShowItemPanel Instance
        {
            get
            {
                if(instance==null)
                {
                    instance=Activator.CreateInstance<ShowItemPanel>();
                }
                return instance;
            }
        }
        #endregion
        public bool IsOpen=false;
        static readonly string path="AssetPackage/GUI/Panel_ShowItem";
        public ShowItemPanel():base(new UIType(path)){}


        #region UI
        TextMeshProUGUI text_NameAndInfo;
        #endregion

        public override void OnEnter()
        {
            text_NameAndInfo=UITool.Instance.GetORAddComponentInChildren<TextMeshProUGUI>("Text_NameAndInfo");
            IsOpen=true;
        }

        public void SetNameAndInfo(string _Name,string _Info)
        {
            text_NameAndInfo.text=_Name+"\n"+_Info;
        }
        public override void OnExit()
        {
            base.OnExit();
            IsOpen=false;
        }
    }
}
