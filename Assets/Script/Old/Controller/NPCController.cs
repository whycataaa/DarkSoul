using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// NPC管理器，主要处理和玩家之间的对话
/// </summary>
public class NPCController : MonoBehaviour
{
    [SerializeField] private int i_Task;//NPC任务序号
    [SerializeField] private TextAsset textFile;//对话的文本
    private GameViewController viewController;
    [SerializeField]private List<string> textList=new List<string>();//对话列表
    [SerializeField]private int index;//每按一下F数值+1，i_Count == s_Talk.Length时对话结束
    [SerializeField]private bool textFinished=true;//对话是否结束
    private string NPC_Name;//Npc名字
    [SerializeField]private float textSpeed=0.05f;//文本速度
    private BagGridControl bagGridControl;
    [SerializeField]private bool isMainTask;
    [SerializeField]bool haveTask;
    [SerializeField]bool canStore=false;
    [SerializeField]bool haveNextTalk;

    void Awake()
    {
        
    }
    void Start()
    {
        bagGridControl=FindObjectOfType(typeof(BagGridControl)) as BagGridControl;
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
        //taskManager=FindObjectOfType(typeof(TaskViewManager)) as TaskViewManager;
        textFinished=true;
        NPC_Name=this.gameObject.name;
        if(textFile!=null)
        {
            GetTextFromFile(textFile);
        }


    }

    IEnumerator SetTextUI()
    {

        textFinished=false;

            viewController.Text_Dialogue.text="";
            if(textList[index].Trim()==NPC_Name)
            {
                viewController.Text_Name_Top.text=NPC_Name;
                if(haveTask)
                {
                    index++;
                }

            }

            if(textList[index].Trim()==GameInfo.GetPlayerName())
            {
                viewController.Text_Name_Top.text=GameInfo.GetPlayerName();
                if(haveTask)
                {
                    index++;
                }
            }

        if(haveTask)
        {
            for(int i=0;i<textList[index].Length;i++)
            {
                viewController.Text_Dialogue.text+=textList[index][i];

                yield return new WaitForSeconds(textSpeed);
            }
                index++;

        }

        if(!haveTask)
        {
            for(int i=0;i<textList[textList.Count-1].Length;i++)
            {
                viewController.Text_Dialogue.text+=textList[textList.Count-1][i];

                yield return new WaitForSeconds(textSpeed);
            }
        }



        textFinished=true;


    }

    /// <summary>
    ///将文本文件转为字符串数组
    /// </summary>
    /// <param name="textFile"></param>
    private void GetTextFromFile(TextAsset textFile)
    {
        textList.Clear();
        index=0;

        var lineData=textFile.text.Split('\n');//按每行切分

        foreach(var line in lineData)
        {
            textList.Add(line);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Player") { return; }
        //靠近NPC显示NPC名字信息
        ShowNPCName();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Player") { return; }


        if(Input.GetKeyDown(KeyCode.C))
        {
            //没任务了并且对话结束
            if(!haveTask&&textFinished)
            {
                //
                if(!viewController.panel_Dialogue.activeSelf)
                {
                    index=textList.Count-2;
                    GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);//停止玩家移动
                    DisShowNPCName();
                    //显示对话框
                    viewController.panel_Dialogue.SetActive(true);
                    StartCoroutine(SetTextUI());
                }

                if(index==textList.Count-2&&textFinished)
                {
                    viewController.panel_Dialogue.SetActive(false);
                    GameObject.Find("Player").GetComponent<AvatarController>().CanMove(true);//取消停止玩家移动
                    return;
                }

            }
            if(haveTask)
            {
                if(index==textList.Count)
                {
                    //根据任务类型放入主线或者支线
                    GetTaskToCurrentTask();


                    haveTask = false;

                viewController.panel_Dialogue.SetActive(false);
                GameObject.Find("Player").GetComponent<AvatarController>().CanMove(true);//取消停止玩家移动

                return;
                }

                if(textFinished)
                {
                    GameObject.Find("Player").GetComponent<AvatarController>().CanMove(false);//停止玩家移动
                    DisShowNPCName();
                    //显示对话框
                    viewController.panel_Dialogue.SetActive(true);
                    StartCoroutine(SetTextUI());
                }
            }


        }
        //玩家按下F或者鼠标左键交互

        //对话结束

    }

    private void GetTaskToCurrentTask()
    {
        if (isMainTask)
        {
      //      taskManager.CopyTaskButton(i_Task, taskManager.currentTaskData_Main);
     //       taskManager.TaskDataDisplay(taskManager.currentTaskData_Main);
        }
        else
        {
      //      taskManager.CopyTaskButton(i_Task, taskManager.currentTaskData_Branch);
      //      taskManager.TaskDataDisplay(taskManager.currentTaskData_Branch);
        }
    }

    private void OnTriggerExit(Collider other)

    {
        DisShowNPCName();
    }
    private void ShowNPCName()
    {
        viewController.panel_ShowNPCName.SetActive(true);
        viewController.Text_NPCName.text=NPC_Name;
    }
    private void DisShowNPCName()
    {
        viewController.panel_ShowNPCName.SetActive(false);
    }


}