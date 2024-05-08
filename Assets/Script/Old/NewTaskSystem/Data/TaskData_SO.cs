using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
 
[CreateAssetMenu(fileName ="New Task",menuName ="Task/Task Data")]
public class TaskData_SO : ScriptableObject
{
    [System.Serializable]
    public class TaskRequire
    {
        public string name;//需求物品、人物、怪物名
        public int requireAmount;//任务需求数量
        public int currentAmount;//当前数量
        public Sprite image;
        public TaskType taskType;//任务类型
        public bool haveDone;//是否完成小任务
    }
 
    //任务名
    public string taskName;
    //任务描述
    [TextArea]
    public string taskDescription;
    public string taskReward;
    //是否完成当前整个任务
    public bool isFinished;
     //任务可能有多个需求
    public List<TaskRequire> taskRequires = new List<TaskRequire>();
    public ItemList_SO bagItem;

    public int taskID;



    public void GiveRewards()
    {
        foreach(var item in bagItem.bagItems)
        {
            int requireCount = Mathf.Abs(item.itemNum);

            //在背包里找是否有该物品，
            if (ItemManager.Instance.FindItemNum(item) != 0)
            {
                //这种情况是背包里的东西不够，那就先在背包里扣除一部分，
                if (item.itemNum <= requireCount)
                {
                    requireCount -= item.itemNum;//所需的数量减少
                }
                //这种情况就是背包里的东西直接够，那直接扣除就好
                else
                {
                    item.itemNum -= requireCount;
                }
            }

 
            BagGridControl.UpdateItemToUI();
        }
    }
 
 
 
}