using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class testscene : MonoBehaviour
{

    public List<GameObject> Obj;

    public GameObject chuansongpos;

    GameObject player;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            var obj = other.gameObject;
            player = obj;
            foreach (var gameObj in Obj)
            {

                DontDestroyOnLoad(gameObj);
            }
            DontDestroyOnLoad(obj);

            LoadSceneManager.Instance.LoadNextLevel(2);
            StartCoroutine(Jiazai());

        }

    }
    IEnumerator Jiazai()
    {
        yield return new WaitForSeconds(2);
        player.transform.position = chuansongpos.transform.position;
    }

}