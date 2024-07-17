using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class toastUI : SingletonMono<toastUI>
{
    public Text T_toast;
    public GameObject tishi;
    public void Showtoast(string a)
    {
        StartCoroutine(ShowT(a));
        
    }


    IEnumerator ShowT(string a)
    {
        T_toast.text = a;
        tishi.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        tishi.SetActive(false);

    }
}
