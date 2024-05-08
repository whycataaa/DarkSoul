using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 任务UI的管理
/// </summary>
public class TaskUI : Singleton<TaskUI>
{
    [Header("组件获取")]
    [Header("小任务面板")]
    public GameObject taskMINI;
    public Text taskNameMINI;
    public Text taskDescriptionMINI;
    public Text taskRewardMINI;
    public GameObject taskTargetMINIPrefab;
    public RectTransform panel_TargetTrans;
    [Header("大任务面板")]
    public GameObject taskBIG;
    //生成按钮的位置
    public RectTransform taskButtonTrans;
    public GameObject taskNameButtonPrefab;
    public RectTransform taskDetailTrans;
    public GameObject showTaskDetailPrefab;
    [HideInInspector] public GameObject currentTaskDetail;
    [HideInInspector] public Button Bt_Tracking;
    [Header("当前任务信息")]
    public Text taskName;
    public Text taskInfo;
    public Text taskReward;
    public TaskData_SO currentData;
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            Cursor.visible=!Cursor.visible;
            Cursor.lockState = CursorLockMode.None;
            taskBIG.SetActive(!taskBIG.activeSelf);
        }
    }

    /// <summary>
    /// 任务面板初始化
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        currentTaskDetail = Instantiate(showTaskDetailPrefab, taskDetailTrans);
        Bt_Tracking = currentTaskDetail.GetComponentInChildren<Button>();
        taskName = currentTaskDetail.transform.GetChild(0).GetComponent<Text>();
        taskInfo = currentTaskDetail.transform.GetChild(1).GetComponent<Text>();
        taskReward = currentTaskDetail.transform.GetChild(2).GetComponent<Text>();
        Bt_Tracking.onClick.AddListener(OnClickTrackingButton);
        currentTaskDetail.SetActive(false);
        UpdateBIGTaskUI();
        taskBIG.SetActive(false);
        taskMINI.SetActive(false);
    }
    /// <summary>
    /// 更新大任务面板的UI
    /// </summary>
    public void UpdateBIGTaskUI()
    {
        //先移除所有任务按钮
        RemoveAllChildren(taskButtonTrans.gameObject);
        currentTaskDetail.SetActive(false);
        //获取任务数据
        var taskList = TaskManager.Instance.tasks;
        if (taskList.Count > 0)
        {
            foreach (var task in taskList)
            {
                var btControl = Instantiate(taskNameButtonPrefab, taskButtonTrans)
                              .GetComponent<TaskNameButton>();
                //将任务信息赋给按钮
                btControl.currentTaskData = task;
                btControl.gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = task.taskName;
            }
        }
    }
    /// <summary>
    /// 更新任务详情
    /// </summary>
    /// <param name="taskData"></param>
    public void UpdateTaskDetailUI(TaskData_SO taskData)
    {
        if (currentTaskDetail.activeSelf)
        {
            taskName.text = taskData.taskName;
            taskInfo.text = taskData.taskDescription;
            taskReward.text = taskData.taskReward;
        }

    }
    /// <summary>
    /// 按下追踪任务按钮触发
    /// </summary>
    public void OnClickTrackingButton()
    {
        taskMINI.SetActive(true);
        UpdateTaskMINIUI();
    }
    /// <summary>
    /// 更新小任务窗口
    /// </summary>
    public void UpdateTaskMINIUI()
    {
        if (currentData != null)
        {
            ClearTaskMINIInfoUI();
            taskNameMINI.text = currentData.taskName;
            taskDescriptionMINI.text = currentData.taskDescription;
            taskRewardMINI.color = Color.red;
            taskRewardMINI.text = currentData.taskReward;

            foreach (var require in currentData.taskRequires)
            {
                var obj = Instantiate(taskTargetMINIPrefab, panel_TargetTrans);
                obj.GetComponent<Image>().sprite = require.image;
                var t = obj.GetComponentInChildren<Text>();

                if (require.taskType == TaskType.searchPerson)
                {
                    t.text = "";
                }
                else
                {
                    t.text = $"<color=#FF0000>{require.currentAmount}</color>" + $"/<color=#FF0000>{require.requireAmount}</color>";
                }

            }
        }
    }

    /// <summary>
    /// 清空小任务信息
    /// </summary>
    private void ClearTaskMINIInfoUI()
    {
        taskNameMINI.text = "";
        taskDescriptionMINI.text = "";
        RemoveAllChildren(panel_TargetTrans.gameObject);
    }
    public static void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }

    public IEnumerator DisplayFinishedTask(TaskData_SO task)
    {
        ClearTaskMINIInfoUI();
        taskNameMINI.text = taskName.text;
        taskDescriptionMINI.text = taskInfo.text;

        foreach (var require in currentData.taskRequires)
        {
            var obj = Instantiate(taskTargetMINIPrefab, panel_TargetTrans);
            obj.GetComponent<Image>().sprite = require.image;
            var t = obj.GetComponentInChildren<Text>();
            if (require.taskType == TaskType.searchPerson)
            {
                t.text = "";
            }
            else
            {
                t.color = Color.green;
                t.text = require.currentAmount + "/" + require.requireAmount;
            }

        }
        taskRewardMINI.color = Color.green;
        taskRewardMINI.text = taskReward.text;
        Debug.Log("changed");
        yield return new WaitForSeconds(2f);

        toastUI.Instance.Showtoast(task.taskName + "已完成");
        ClearTaskMINIInfoUI();
        //提示完成任务
        taskMINI.SetActive(false);

        currentData.isFinished = false;
    }
}
