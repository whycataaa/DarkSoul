using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class DialogueUI : SingletonMono<DialogueUI>
{
    [Header("对话信息")]
    //人物头像图片
    public Image icon;
    //对话文本
    public Text mainText;
    //说话角色名字
    public Text TalkerText;
    //下个对话按钮
    public Button nextButton;
    //对话面板
    public GameObject dialoguePanel;

    public GameObject panel_ShowNPC;
    [Range(0,1)]
    public float textSpeed=0.1f;
    [Header("Data")]
    public DialogueData_SO currentData;
    [SerializeField]int currentIndex=0;

    [Header("Choices")]
    public RectTransform ChoicePanelTrans;
    public ChoiceUI choicePrefab;
    
    protected override void Awake()
    {
        base.Awake();
        panel_ShowNPC.SetActive(false);
        dialoguePanel.SetActive(false);
        nextButton.onClick.AddListener(ContinueDialogue);
    }
    /// <summary>
    /// 更新对话数据
    /// </summary>
    /// <param name="data"></param>
    public void UpdateDialogueData(DialogueData_SO data)
    {
        Instance.currentData = null;
        Instance.currentData=data;
        Instance.currentIndex = 0;
    }
    /// <summary>
    /// 打开对话面板，根据每句对话的信息对文本，头像进行更新
    /// </summary>
    /// <param name="piece"></param>
    public void UpdateMainDialogue(DialoguePiece piece)
    {
        dialoguePanel.SetActive(true);
        currentIndex++;
        if(piece.image!=null)
        {
            icon.enabled=true;
            icon.sprite=piece.image;
        }
        else
        {
            icon.enabled=false;
        }
        TalkerText.text=piece.Name;
        mainText.text="";//先清空对话
        //mainText.text=piece.text;
        mainText.DOText(piece.text,textSpeed);
        //可以继续对话继续按钮亮起
        if(piece.options.Count==0&&currentData.dialoguePieces.Count>0)
        {
            nextButton.interactable=true;
            nextButton.gameObject.SetActive(true);
        }
        else
        {
            //nextButton.gameObject.SetActive(false);
            nextButton.transform.GetChild(0).gameObject.SetActive(false);
            nextButton.interactable=false;
        }
        CreateChoice(piece);
    }

    /// <summary>
    /// 按下按钮继续对话
    /// </summary>
    void ContinueDialogue()
    {
        if(currentIndex<currentData.dialoguePieces.Count)
        {
            //如果还有对话继续
            UpdateMainDialogue(currentData.dialoguePieces[currentIndex]);
        }
        else
        {
            //没有对话关闭面板
            dialoguePanel.SetActive(false);
            CameraControl.Instance.EndTalk();
        }
    }
    /// <summary>
    /// 选项的创建（先销毁再创建）
    /// </summary>
    void CreateChoice(DialoguePiece piece)
    {
        //销毁
        if(ChoicePanelTrans.childCount>0)
        {
            for(int i=0;i<ChoicePanelTrans.childCount;i++)
            {
                Destroy(ChoicePanelTrans.GetChild(i).gameObject);
            }
        }
        ChoicePanelTrans.gameObject.SetActive(true);
        //生成
        for(int i=0;i<piece.options.Count;i++)
        {
            var choice=Instantiate(choicePrefab,ChoicePanelTrans);
            choice.UpdateChoice(piece,piece.options[i]);
        }
    }
}
