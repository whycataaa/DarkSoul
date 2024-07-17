using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif


namespace PolygonProject
{
    public class ResManager : SingletonMono<ResManager>
    {

    /// <summary>
    /// 加载资源
    /// </summary>
    /// <param name="_name">资源在AssetPackage下的路径</param>
    /// <typeparam name="T">资源类型</typeparam>
    /// <returns></returns>
    public T LoadResource<T>(string _name,string _resName) where T : UnityEngine.Object
    {

#if UNITY_EDITOR
        string path="Assets/AssetPackage/"+_name+"/"+_resName;

        return AssetDatabase.LoadAssetAtPath<T>(path);
#else
        return ABMgr.Instance.LoadRes<T>(_name,_resName);
#endif

    }
    }
}
