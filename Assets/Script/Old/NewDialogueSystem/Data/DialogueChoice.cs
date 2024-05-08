using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogueChoice : MonoBehaviour
{
    public string text;
    public string targetID;
    public bool getTask;
    public bool getGame;
    public bool openStore;
}