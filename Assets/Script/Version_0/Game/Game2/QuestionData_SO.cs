using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Question/New Question")]
public class QuestionData_SO : ScriptableObject
{
    [TextArea]
    public string Q_question;

    [System.Serializable]
    public class Q_Option
    {
        [TextArea]
        public string option;
        public bool isTrueAnswer;
    }

    public List<Q_Option> options;

    [TextArea]
    public string optionTip;
}
