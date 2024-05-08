using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 任务面板的任务按钮，点击可查看任务
/// </summary>
public class TaskNameButton : MonoBehaviour
{

    public Text taskName;
    //存储任务信息
    public TaskData_SO currentTaskData;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        GetComponent<Button>().onClick.AddListener(OnClickTaskButton);
    }
    public void OnClickTaskButton()
    {
        if(!TaskUI.Instance.currentTaskDetail.activeSelf)
        {
            TaskUI.Instance.currentTaskDetail.SetActive(true);
        }
        TaskUI.Instance.UpdateTaskDetailUI(currentTaskData);
        TaskUI.Instance.currentData=this.currentTaskData;
    }

}
