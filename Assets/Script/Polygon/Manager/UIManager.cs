using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class UIManager:Singleton<UIManager>
    {

        //每一个UI对应一个GameObject
        public Dictionary<UIType,GameObject> UIDic;

        public UIManager()
        {
            UIDic=new Dictionary<UIType, GameObject>();
        }


        /// <summary>
        /// 获取一个UI的GameObject并显示
        /// </summary>
        /// <param name="_UIType"></param>
        /// <returns></returns>
        public GameObject GetAndShowUI(UIType _UIType)
        {
            GameObject go=GameObject.Find("UIRoot");
            if(!go)
            {
                Debug.Log("UIRoot不存在!");
                return null;
            }

            //字典里有直接显示就行
            if(UIDic.ContainsKey(_UIType))
            {
                UIDic[_UIType].SetActive(true);
                return UIDic[_UIType];
            }
            else
            {
                //没有再实例化一个出来
                GameObject UI=GameObject.Instantiate(ResManager.Instance.LoadResource<GameObject>("GUI",_UIType.Name+".prefab"),go.transform);
                UI.name=_UIType.Name;
                UIDic.Add(_UIType,UI);
                return UI;
            }
        }
        /// <summary>
        /// 取消显示UI
        /// </summary>
        public void DisShowUI(UIType _UIType)
        {
            if(UIDic.ContainsKey(_UIType))
            {
                UIDic[_UIType].SetActive(false);
            }
        }
    }

}
