
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using echo17.EndlessBook.Demo03;
/// <summary>
/// 任务数据和UI的控制
/// </summary>
/// 
public class TaskManager : Singleton<TaskManager>
{
    //任务列表
    public List<TaskData_SO> tasks = new List<TaskData_SO>();


    [Header("任务1")]
    public Item_SO item1;
    [Header("任务4")]
    public Item_SO item4;


    public GameObject jineng;
    [Header("任务4")]
    public Item_SO item6;
    /// <summary>
    /// 敌人死亡，物品拾取时调用
    /// </summary>
    /// <param name="requireName"></param>
    /// <param name="requireNum"></param>
    public void UpdateTaskData(string requireName, int requireNum)
    {
        foreach (var task in tasks)
        {
            if (task.isFinished)
                continue;
            var matchTask = task.taskRequires.Find(r => r.name == requireName);
            Debug.Log(matchTask);
            if (matchTask != null)
            {
                matchTask.currentAmount += requireNum;
            }

        }
        CheckAllTask();
        TaskUI.Instance.UpdateTaskMINIUI();
    }
    /// <summary>
    /// 判断是否有这个任务
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public bool HaveTaskData(TaskData_SO data)
    {
        if (data != null)
            return tasks.Any(q => q.taskName == data.taskName);
        else return false;
    }
    /// <summary>
    /// 获取任务，加入当前任务列表
    /// </summary>
    /// <param name="taskData"></param>
    public void GetTaskData(TaskData_SO taskData)
    {
        Instance.tasks.Add(taskData);
    }
    /// <summary>
    /// 移除任务数据
    /// </summary>
    /// <param name="taskData"></param>
    public void DeleteTaskData(TaskData_SO taskData)
    {
        Instance.tasks.Remove(taskData);
    }

    List<TaskData_SO> tasksToRemove = new List<TaskData_SO>();

    /// <summary>
    /// 检测当前所有的任务
    /// </summary>
    public void CheckAllTask()
    {
        if (tasks.Count > 0)
        {
            foreach (var task in tasks)
            {
                CheckTask(task);
                if (task.isFinished)
                {
                    StartCoroutine(TaskUI.Instance.DisplayFinishedTask(task));
                    // 将需要删除的任务添加到列表中
                    tasksToRemove.Add(task);

                    switch (task.taskID)
                    {
                        case 1:
                            ItemManager.Instance.AddItem(item1);
                            GameInfo.SetCoin(GameInfo.GetCoin() + 1000);
                            BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["1"]);
                            break;
                        case 2:
                            GameInfo.SetCoin(GameInfo.GetCoin() + 1000);
                            BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["2"]);
                            break;
                        case 3:
                            BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["3"]);
                            break;
                        case 4:
                            ItemManager.Instance.AddItem(item4);
                            ItemManager.Instance.AddItem(item4);
                            GameInfo.SetCoin(GameInfo.GetCoin() + 500);
                            jineng.SetActive(true);
                            BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["4"]);
                            break;
                        case 5:
                            GameInfo.SetCoin(GameInfo.GetCoin() + 1500);
                            break;
                        case 6:
                            BookControl.Instance.endlessBook.AddPageData(BookControl.Instance.pageTable["6"]);
                            ItemManager.Instance.AddItem(item6);
                            break;
                    }



                }
            }

            if (tasksToRemove.Count > 0)
            {
                // 遍历完成后删除需要删除的任务
                foreach (var taskToRemove in tasksToRemove)
                {
                    DeleteTaskData(taskToRemove);
                    TaskUI.Instance.UpdateBIGTaskUI();
                }
            }
        }

   

    }
    /// <summary>
    /// 给定任务数据，对任务进行检测
    /// </summary>
    /// <param name="taskData"></param>
    private void CheckTask(TaskData_SO taskData)
    {
        foreach (var require in taskData.taskRequires)
        {
            if (require.taskType == TaskType.searchPerson)
            {
                if(GameObject.Find(require.name)!=null)
                {
                     var NPCPos = GameObject.Find(require.name).transform.position;
                    var PlayerPos = GameInfo.GetPos();
                    require.haveDone = (NPCPos - PlayerPos).magnitude < 4;
                }


            }
            if (require.taskType == TaskType.exterminate || require.taskType == TaskType.collect)
            {
                //完成某个
                var finishRequires = taskData.taskRequires.Where(r => r.requireAmount <= r.currentAmount);
                require.haveDone = finishRequires.Count() == taskData.taskRequires.Count;
            }
        }
        for (int i = 0; i < taskData.taskRequires.Count; i++)
        {
            if (taskData.taskRequires[i].haveDone == false)
            {
                taskData.isFinished = false;
            }
            else
            {
                taskData.isFinished = true;
            }
        }

        TaskUI.Instance.UpdateTaskMINIUI();
    }
    /*

                //当前任务需要收集/消灭的目标名字列表
                public List<string> RequireTargetName(TaskData_SO taskData)
                {
                    List<string> targetNameList = new List<string>();
                    foreach(var require in taskData.taskRequires)
                    {
                        targetNameList.Add(require.name);
                    }
                    return targetNameList;
                }*/
}

