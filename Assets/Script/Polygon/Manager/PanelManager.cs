using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PanelManager : Singleton<PanelManager>
    {
        private Stack<BasePanel> panelStack;
        private BasePanel panel;

        public PanelManager()
        {
            panelStack=new Stack<BasePanel>();
        }

        /// <summary>
        /// 仅仅显示一个面板
        /// </summary>
        public void ShowPanel(BasePanel _Panel)
        {
            UIManager.Instance.GetAndShowUI(_Panel.UIType);
            _Panel.OnEnter();
        }
        /// <summary>
        /// 仅仅关闭（active设为false）一个面板
        /// </summary>
        public void DisShowPanel(BasePanel _Panel)
        {
            _Panel.OnExit();
        }
        /// <summary>
        /// 打开一个面板
        /// </summary>
        /// <param name="nextPanel"></param>
        public void PanelPush(BasePanel nextPanel)
        {
            //有UI
            if(panelStack.Count>0)
            {
                panel=panelStack.Peek();
                //栈顶的UI暂停
                panel.OnPause();
            }
            //UI面板入栈
            panelStack.Push(nextPanel);

            UIManager.Instance.GetAndShowUI(nextPanel.UIType);

            nextPanel.OnEnter();
            //Debug.Log(panelStack.Peek()+"入栈");
        }

        /// <summary>
        /// 关闭当前面板
        /// </summary>
        public void PanelPop()
        {
            //Debug.Log(panelStack.Peek()+"出栈");
            if(panelStack.Count>0)
            {
                panelStack.Peek().OnExit();
                panelStack.Pop();
            }
            if(panelStack.Count>0)
            {
                //Debug.Log(panelStack.Peek()+"继续");
                panelStack.Peek().OnResume();
            }

            
        }

        public Stack<BasePanel> GetPanelStack()
        {
            return panelStack;
        }
    }
}
