using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PolygonProject
{
    /// <summary>
    /// 所有UI面板的父类
    /// </summary>
    public abstract class BasePanel
    {
        public UIType UIType{get;private set;}

        //UI控件
        protected Button[] buttonList;
        protected TextMeshProUGUI[] TMProList;
        public BasePanel(UIType _UIType)
        {
            UIType=_UIType;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        public virtual void Init()
        {

        }
        /// <summary>
        /// 开始执行一次
        /// </summary>
        public virtual void OnEnter()
        {

        }
        /// <summary>
        /// 暂停时执行
        /// </summary>
        public virtual void OnPause()
        {
            UITool.Instance.GetORAddComponent<CanvasGroup>().blocksRaycasts=false;
        }
        /// <summary>
        /// 继续时执行
        /// </summary>
        public virtual void OnResume()
        {
            UITool.Instance.GetORAddComponent<CanvasGroup>().blocksRaycasts=true;
        }
        /// <summary>
        /// 退出时执行
        /// </summary>
        public virtual void OnExit()
        {
            UIManager.Instance.DisShowUI(UIType);
        }
    }
}
