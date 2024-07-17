using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SkillController : MonoBehaviour
{
    [SerializeField] private float cd = 1;

    //private bool isFillImage;
    // Start is called before the first frame update
    Image imgMask;
    void Start()
    {
        imgMask = GetComponent<Image>();
        //isFillImage = false;
    }

    public void UseSkill()
    {
        if (imgMask.fillAmount > 0)
        {
            return;
        }
        StartCoroutine(UseSkillCor());
    }

    IEnumerator UseSkillCor()
    {
        float workTime = 0;
        while (true)
        {
            workTime += Time.deltaTime;
            imgMask.fillAmount = Mathf.Lerp(1, 0, workTime / cd);
            if (workTime / cd >= 1)
            {
                break;
            }
            yield return null;//暂停协程等待下一帧继续执行
        }
    }

}
