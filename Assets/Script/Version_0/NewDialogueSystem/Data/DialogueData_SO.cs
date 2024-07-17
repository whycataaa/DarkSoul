using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 存储每个角色对话信息
/// </summary>
[CreateAssetMenu(fileName ="New Dialogue",menuName ="Dialogue/Dialogue Data")]
public class DialogueData_SO : ScriptableObject
{
    public Sprite dx;

    public List<DialoguePiece> dialoguePieces = new List<DialoguePiece>();
    public Dictionary<string, DialoguePiece> dialogueIndex = new Dictionary<string, DialoguePiece>();

    
    //如果是在Unity编辑器中，则字典随时改变时则进行修改，如果是打包则字典信息不会更改
#if UNITY_EDITOR
    void OnValidate()//一旦这个脚本中的数据被更改时会自动调用
    {
        

        dialogueIndex.Clear();
        //一旦信息有所更新，就会将信息存储在字典中
        foreach(var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
#else
    void Awake()//保证在打包执行的游戏里第一时间获得对话的所有字典匹配 
    {
        dialogueIndex.Clear();
        foreach (var piece in dialoguePieces)
        {
            if (!dialogueIndex.ContainsKey(piece.ID))
                dialogueIndex.Add(piece.ID, piece);
        }
    }
#endif


}
