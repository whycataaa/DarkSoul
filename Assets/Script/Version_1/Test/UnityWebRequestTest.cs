using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UnityWebRequestTest : MonoBehaviour
{
    // Start is called before the first frame update
    public AssetBundle bundle;
    GameObject go;
    void Start()
    {
        Debug.Log("ee");


    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator LoadAssetBundleFromURL(string url)
    {
        UnityWebRequest uwr=UnityWebRequestAssetBundle.GetAssetBundle(url);
        yield return uwr.SendWebRequest();
        if (uwr.result == UnityWebRequest.Result.Success)
        {
            bundle = DownloadHandlerAssetBundle.GetContent(uwr);
            Debug.Log("读取AssetBundle成功");

            go = bundle.LoadAsset<GameObject>("GameObject");
            Debug.Log(go == null);
            Instantiate(go, new Vector3(0, 0, 0), Quaternion.identity);
        }
        else
        {
            Debug.Log("下载AssetBundle出错: " + uwr.error);
        }
        bundle.Unload(false);
    }
}


