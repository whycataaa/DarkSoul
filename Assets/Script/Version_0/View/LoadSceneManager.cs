using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneManager : SingletonMono<LoadSceneManager>
{
    public GameObject loadScreen;//��ʾ�����������

    public GameObject cavans;

    public GameObject start;
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
        DontDestroyOnLoad(cavans);
        loadScreen.SetActive(false);
//        start.SetActive(true);
    }
    //�����˸���ť��ӵ���¼�
    public void LoadNextLevel(int sceneNum)
    {
        StartUI.Instance.panel_start.SetActive(false);
        StartCoroutine(Loadlevel(sceneNum));
    }
    IEnumerator Loadlevel(int sceneNum)
    {
        loadScreen.SetActive(true);

        yield return new WaitForSeconds(2f);


        SceneManager.LoadScene(sceneNum);
        yield return new WaitForSeconds(1f);
        loadScreen.SetActive(false);
    }
}


