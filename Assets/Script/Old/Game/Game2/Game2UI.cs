using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game2UI : Singleton<Game2UI>
{
    [SerializeField] private int TIMUNum;//题目的数量
    //题目的数据
    public QuestionData_SO[] questionDatas;
    public int index = 0;
    public QuestionData_SO currentQData;

    public Text T_Question;
    public Text T_A;
    public Text T_B;
    public Text T_C;
    public Text T_D;
    public Text T_Tips;

    public Button Bt_A;
    public Button Bt_B;
    public Button Bt_C;
    public Button Bt_D;
    public Button Bt_Next;

    public Image A;
    public Image B;
    public Image C;
    public Image D;

    public Sprite IStrue;
    public Sprite ISFalse;

    public bool IsRight;
    public void StartGame2()
    {
        Instance.currentQData = questionDatas[index];
        Instance.UpdateQUI();
        Bt_A.onClick.AddListener(() => OnOptionSelected(0));
        Bt_B.onClick.AddListener(() => OnOptionSelected(1));
        Bt_C.onClick.AddListener(() => OnOptionSelected(2));
        Bt_D.onClick.AddListener(() => OnOptionSelected(3));

        Bt_Next.onClick.AddListener(NextQuestion);
    }
    public void UpdateQUI()
    {
        Bt_Next.gameObject.SetActive(false);
        T_Tips.gameObject.SetActive(false);
        T_Question.text = currentQData.Q_question;
        T_A.text = "A."+currentQData.options[0].option;
        T_B.text = "B."+currentQData.options[1].option;
        T_C.text = "C."+currentQData.options[2].option;
        T_D.text = "D."+currentQData.options[3].option;
        T_Tips.text = currentQData.optionTip;

    }

    private void NextQuestion()
    {
        A.gameObject.SetActive(false);
        B.gameObject.SetActive(false);
        C.gameObject.SetActive(false);
        D.gameObject.SetActive(false);
        index++;
        if(!IsRight)
        {
            index--;
            Instance.currentQData = questionDatas[index];
            Instance.UpdateQUI();
        }
        else
        {
            if(index>=TIMUNum)
            {
                //结束游戏
                Game2DPanel_Control.Instance.currentDialogue.CurrentData = Game2DPanel_Control.Instance.currentDialogue.GetComponent<TaskGiver>().ISFinishGameDialogue;
                Game2DPanel_Control.Instance.panel_Game2dBG.SetActive(false);
                return;
            }
            if(IsRight)
            {
                Debug.Log("yes");
                Instance.currentQData = questionDatas[index];
                Instance.UpdateQUI();
            }
        }

    }

    private void OnOptionSelected(int optionIndex)
    {
        bool selectedOptionIsTrue = currentQData.options[optionIndex].isTrueAnswer;

        if (selectedOptionIsTrue)
        {
            // 选项正确时的处理
            Bt_Next.GetComponentInChildren<Text>().text = "继续";
            switch (optionIndex)
            {
                case 0:
                    A.gameObject.SetActive(true);
                    A.sprite = IStrue;
                    break;
                case 1: 
                    B.gameObject.SetActive(true);
                    B.sprite = IStrue;
                    break;
                case 2:
                    C.gameObject.SetActive(true);
                    C.sprite = IStrue;
                    break;
                case 3:
                    D.gameObject.SetActive(true);
                    D.sprite = IStrue;
                    break;
            }

            //正确时的UI
            IsRight = true;
            T_Tips.gameObject.SetActive(true);
            Bt_Next.gameObject.SetActive(true);
        }
        else
        {
            switch (optionIndex)
            {
                case 0:
                    A.gameObject.SetActive(true);
                    A.sprite = ISFalse;
                    break;
                case 1:
                    B.gameObject.SetActive(true);
                    B.sprite = ISFalse;
                    break;
                case 2:
                    C.gameObject.SetActive(true);
                    C.sprite = ISFalse;
                    break;
                case 3:
                    D.gameObject.SetActive(true);
                    D.sprite = ISFalse;
                    break;
            }
            // 选项错误时的处理
            //错误时的UI
            Bt_Next.GetComponentInChildren<Text>().text = "重选";
            IsRight = false;
            T_Tips.gameObject.SetActive(true);
            Bt_Next.gameObject.SetActive(true);

        }
    }
}
