using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 死亡界面，自动跳转到新手村
/// </summary>
public class Panel_DieView : MonoBehaviour
{
    [SerializeField] private string s_Scene;

    private void OnEnable()
    {
        StartCoroutine(SceneJump());
    }

    IEnumerator SceneJump()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(s_Scene);
    }
}