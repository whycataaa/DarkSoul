using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2DPanel_Control : SingletonMono<Game2DPanel_Control>
{
    private readonly string GAME1ANWSER1 = "37/4";
    private readonly string GAME1ANWSER2 = "17/4";
    private readonly string GAME1ANWSER3 = "11/4";
    public GameObject panel_Game2dBG;
    public GameObject panel_Current;
    [SerializeField] private string[] gameName;
    [SerializeField] private GameObject[] gamePanels;
    [SerializeField] Dictionary<string, GameObject> game = new Dictionary<string, GameObject>();

    public NPCDialogueControl currentDialogue;
    [Header("Game1")]
    //������ı�
    public Text inputText;
    //��ⰴť
    public Button checkButton;
    //������ı�
    public Text inputText2;
    //������ı�
    public Text inputText3;


    protected override void Awake()
    {

        base.Awake();
        checkButton.onClick.AddListener(Game1Check);
        if (gameName.Length == gamePanels.Length)
        {
            for (int i = 0; i < gameName.Length; i++)
            {
                // ����ֵ����Ƿ��Ѿ�������ͬ�ļ�
                if (!game.ContainsKey(gameName[i]))
                {
                    game.Add(gameName[i], gamePanels[i]);
                }
                else
                {
                    Debug.LogWarning("�ֵ����Ѿ����ڼ���" + gameName[i]);
                }
            }
        }
        else
        {
            Debug.LogError("gameName ����� gamePanel ����ĳ��Ȳ�һ�£�");
        }

    }

    private void Game1Check()
    {
        if (inputText.text == GAME1ANWSER1)
        {
            //
            if (inputText2.text == GAME1ANWSER2)
            {
                //
                if (inputText3.text == GAME1ANWSER3)
                {
                    Debug.Log("yes");
                    //������Ϸ�ı�Ի�
                    Debug.Log(currentDialogue.GetComponent<TaskGiver>().ISFinishGameDialogue.GetType());
                    Debug.Log(currentDialogue.CurrentData.GetType());
                    currentDialogue.CurrentData = currentDialogue.GetComponent<TaskGiver>().ISFinishGameDialogue;
                    panel_Game2dBG.SetActive(false);
                }
                else
                {
                    inputText.text = "";
                    inputText2.text = "";
                    inputText3.text = "";
                }
            }
            else
            {
                inputText.text = "";
                inputText2.text = "";
                inputText3.text = "";
            }


        }
        else
        {
            inputText.text = "";
            inputText2.text = "";
            inputText3.text = "";
        }

    }

    private void Start()
    {
        ShowPanel(gamePanels[0]);
        panel_Game2dBG.SetActive(false);
    }
    public void Start2DGame(string gameName)
    {
        panel_Game2dBG.SetActive(true);

        ShowPanel(game[gameName]);
        if (gameName == "Game2")
        {
            Game2UI.Instance.StartGame2();
        }



        /* Debug.Log("start game");
         foreach(var panel in gamePanel) 
         {
             Debug.Log(gameName);
             if (game[gameName]==panel)
             {
                 if(gameName=="Game2")
                 {
                     panel_Game2dBG.SetActive(true);
                     Game2UI.Instance.StartGame2();
                     continue;
                 }
                 panel_Game2dBG.SetActive(true);
                 panel.SetActive(true);
             }
             else
             {
                 panel.SetActive(false);
             }
         }*/
    }
    //��ʾ��ǰ���
    public void ShowPanel(GameObject panelToShow)
    {
        foreach (GameObject panel in gamePanels)
        {
            // ֻ��ʾ�������壬�����Ķ�����
            panel.SetActive(panel == panelToShow);
        }
    }
}
