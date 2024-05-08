using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 选项的UI显示
/// </summary>
public class ChoiceUI : MonoBehaviour
{

    public Text ChoiceText;
    private Button bt_ChoiceButton;
    private DialoguePiece currentPiece;
    private bool getTask;
    private bool getGame;
    private string gameName;
    private bool openStore;
    private string nextDialogueID;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        bt_ChoiceButton = GetComponent<Button>();
        bt_ChoiceButton.onClick.AddListener(OnChoiceClicked);
    }

    public void UpdateChoice(DialoguePiece piece, DialogueOption option)
    {
        currentPiece = piece;
        ChoiceText.text = option.text;
        nextDialogueID = option.targetID;
        getTask = option.getTask;
        openStore = option.openStore;
        getGame = option.getGame;
        gameName = option.gameName;
    }

    void OnChoiceClicked()
    {

        if (openStore)
        {
            Debug.Log("openstore");
            Panel_StoreView.Instance.OpenStoreUI();
        }
        if (getGame)
        {
            Game2DPanel_Control.Instance.Start2DGame(gameName);
        }
        if (currentPiece.taskData_SO != null)
        {
            if (getTask)
            {
                if (!TaskManager.Instance.HaveTaskData(currentPiece.taskData_SO))
                {

                    TaskUI.Instance.currentData = currentPiece.taskData_SO;
                    toastUI.Instance.Showtoast("获得任务:"+currentPiece.taskData_SO.taskName);
                    TaskManager.Instance.GetTaskData(currentPiece.taskData_SO);
                    TaskUI.Instance.UpdateBIGTaskUI();
                    TaskUI.Instance.UpdateTaskDetailUI(currentPiece.taskData_SO);
                //    TaskUI.Instance.OnClickTrackingButton();
                   

                }
            }

        }

        if (nextDialogueID == "")
        {
            DialogueUI.Instance.dialoguePanel.SetActive(false);
            this.transform.parent.gameObject.SetActive(false);
            CameraControl.Instance.EndTalk();
            return;
        }
        else
        {
            DialogueUI.Instance.UpdateMainDialogue(DialogueUI.Instance.currentData.dialogueIndex[nextDialogueID]);
        }
    }

}
