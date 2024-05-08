using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 每句话及每句话的选项、下句话
/// </summary>
[System.Serializable]
public class DialoguePiece {
    public string Name;
    public string ID;
    public Sprite image;
    [TextArea]
    public string text;
    public TaskData_SO taskData_SO;

    public List<DialogueOption> options = new List<DialogueOption>();


}
[System.Serializable]
public class DialogueOption
{
    [Header("选项文本")]
    public string text;
    [Header("跳转到ID为x的对话")]
    public string targetID;
    [Header("跳转的游戏名")]
    public string gameName;
    [Header("是否获取任务")]
    public bool getTask;
    [Header("是否获取游戏")]
    public bool getGame;
    [Header("是否打开商店")]
    public bool openStore;

}

