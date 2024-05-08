using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 挂载在NPC上实现任务功能
/// </summary>
[RequireComponent(typeof(NPCDialogueControl))]
public class TaskGiver : MonoBehaviour
{
    NPCDialogueControl dialogueControl;
    TaskData_SO taskData;
    //未完成和完成时的对话
    public DialogueData_SO UNFinishedTaskDialogue;
    public DialogueData_SO ISFinishedTaskDialogue;

    public DialogueData_SO ISFinishGameDialogue;
    public bool IsFinished
    {
        get
        {
            if (TaskManager.Instance.HaveTaskData(taskData))
            {
                return taskData.isFinished;
            }
            else return false;
        }
    }
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        dialogueControl=GetComponent<NPCDialogueControl>();
        dialogueControl.CurrentData = UNFinishedTaskDialogue;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

    }
}
