using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class UITool:Singleton<UITool>
    {

        GameObject UIRoot=>GameObject.Find("UIRoot");
        public GameObject activePanel
        {
            get
            {
                return UIManager.Instance.UIDic[PanelManager.Instance.GetPanelStack().Peek().UIType];
            }
        }

        public T GetORAddComponent<T>() where T :Component
        {

            if(activePanel.GetComponent<T>()==null)
            {
                activePanel.AddComponent<T>();
            }

            return activePanel.GetComponent<T>();
        }

        public GameObject FindChildGameObject(string name)
        {
            var x=TransformHelper.FindDeepTransform<Transform>(UIRoot.transform,name);

            if (x.gameObject==null)
            {
                Debug.Log($"找不到名为{name}的物体");
                return null;
            }
            else
            {
                return x.gameObject;
            }
        }

        public T GetORAddComponentInChildren<T>(string name) where T:Component
        {
            GameObject go=FindChildGameObject(name);

            if(go.GetComponent<T>()==null)
            {
                return go.AddComponent<T>();
            }
            else
            {
                return go.GetComponent<T>();
            }

        }
    }
}
