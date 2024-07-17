using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartUI : SingletonMono<StartUI>
{

    public GameObject panel_set;
    public GameObject panel_start;
    public Button start;
    public Button end;
    public Button setting;

    public Button exitset;
    protected override void Awake()
    {
        base.Awake();
        panel_start.SetActive(true);

        start.onClick.AddListener(() => LoadSceneManager.Instance.LoadNextLevel(1));
        end.onClick.AddListener(() => Application.Quit());
        setting.onClick.AddListener(() => panel_set.SetActive(true));
        exitset.onClick.AddListener(() => panel_set.SetActive(false));


    }
}
