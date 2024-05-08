using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏全局控制器
/// </summary>
public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject obj_BornPos, camera_Map;//玩家出生点，小地图摄像机

    private GameObject player;//玩家

    void Start()
    {
        player = GameObject.Find("PlayerArmature");
        //player.name = "Player"; // main character is named by "player"
    }

    void Update()
    {
        GameInfo.SetPos(player.transform.position);
        //使地图摄像机与玩家同步
        camera_Map.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 15, player.transform.position.z);
    }


    //返回出生点
    public void ReturnBornPos()
    {
        player.transform.position = obj_BornPos.transform.position;
        player.transform.rotation = obj_BornPos.transform.rotation;
        player.GetComponent<AvatarController>().CanMove(true);
    }
}