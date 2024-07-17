using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 挂载在NPC上实现对话功能
/// </summary>
public class NPCDialogueControl : MonoBehaviour
{

    public DialogueData_SO CurrentData;//当前进行对话的数据
    [SerializeField]bool canTalk=false;

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")&&CurrentData!=null)
        {
            Game2DPanel_Control.Instance.currentDialogue = this;
            canTalk=true;
            ShowNPCName();

        }
    }

    /// <summary>
    /// OnTriggerStay is called once per frame for every Collider other
    /// that is touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {


        if(canTalk&&Input.GetKeyDown(KeyCode.F))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            CameraControl.Instance.StartTalk();
            TaskManager.Instance.CheckAllTask();
            DisShowNPCName();
            openDialogue();
        }
    }
    /// <summary>
    /// OnTriggerExit is called when the Collider other has stopped touching the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            DisShowNPCName();
            canTalk=false;
        }
    }
    /// <summary>
    /// 打开对话面板
    /// </summary>
    void openDialogue()
    {
        //先更新数据再更新文本
        DialogueUI.Instance.UpdateDialogueData(this.CurrentData);
        DialogueUI.Instance.UpdateMainDialogue(CurrentData.dialoguePieces[0]);
    }
    void ShowNPCName()
    {
        DialogueUI.Instance.panel_ShowNPC.SetActive(true);
        DialogueUI.Instance.panel_ShowNPC.GetComponentInChildren<Text>().text=this.gameObject.name;
    }
    void DisShowNPCName()
    {
        DialogueUI.Instance.panel_ShowNPC.SetActive(false);
    }
}
