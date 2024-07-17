using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人控制器
/// </summary>
public class EnemyController : MonoBehaviour
{

    public float HP = 100;
    public int Attack = 20;
    //出生点
    private Vector3 position;
    private Animation animation;
    private GameObject player;
    private Quaternion targetRotation;//设定要旋转到的角度：目标相对于物体的世界坐标系角度
    [SerializeField] private GameObject obj_attackPos, pre_Explosion, pre_Bullet;//敌人攻击位置，爆炸预制体，子弹预制体
    [SerializeField] private int i_SearchDistance = 20, i_AttackDistance = 3;//敌人搜索范围、攻击范围
    [SerializeField] private int i_Life = 100, i_Coin = 100, i_Attack = 10;//血量、击杀得分、伤害值
    private float timer = 1;
    private bool isAttack = false;

    public List<GameObject> itemPrefabs = new List<GameObject>();
    void Start()
    {
        animation = GetComponent<Animation>();
        player = GameObject.Find("PlayerArmature");
        position = transform.position;
    }

    void Update()
    {
        if (player == null) { player = GameObject.Find("PlayerArmature"); return; }
        //获取与玩家的距离
        float distance = Vector3.Distance(player.transform.position, transform.position);

        //如果玩家在搜索范围之外
        if (distance > i_SearchDistance)
        {
            if (Vector3.Distance(transform.position, position) > 1)
            {
                //转向出生点
                transform.LookAt(new Vector3(position.x, transform.position.y, position.z));
                //向前移动
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                //播放移动动画
                //animator.SetBool("Run", true);
                animation.Play("run");
            }
            else
            {
                animation.CrossFade("idle");
                //animator.SetBool("Run", false);
            }

            //animation.CrossFade("idle");
            return;
        }
        else if (distance > i_AttackDistance)
        {
            //朝向玩家移动
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            //移动
            transform.Translate(Vector3.forward * 2 * Time.deltaTime);
            //播放移动动画
            //animator.SetBool("Run", true);
            animation.Play("run");
            isAttack = false;
            //animator.SetBool("Attack", false);
            //animation.Play("Attack");
        }
        else
        {

            //攻击玩家
            //停止移动
            //animator.SetBool("Run", false);
            animation.CrossFade("idle");
            //转向玩家
            transform.LookAt(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z));
            //攻击
            //animator.SetBool("Attack", true);
            animation.Play("attack");
            if (isAttack == false)
            {
                isAttack = true;
                timer = 1;
            }
            //计时器增加
            timer += Time.deltaTime;
            if (timer >= 2)
            {
                timer = 0;
                player.GetComponent<StarterAssets.ThirdPersonControllerCopy>().GetDamage(Attack);
                //if (UIManager.Instance == null) { Debug.Log("null"); }
                UIManager.Instance.UpdateHPBar();
                //Debug.Log("攻击伤害：" + 100);
            }
        }
    }



    //伤害值处理
    void OnDemage(int demage)
    {
        i_Life -= demage;
        if (i_Life <= 0)
        {
            //奖励金币
            GameInfo.SetCoin(i_Coin + GameInfo.GetCoin());
            GameViewController viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();
            viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateCoin();
            //销毁实体，并生成爆炸效果
            Instantiate(pre_Explosion, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.rotation);
            Destroy(gameObject);
        }
    }

    //转向玩家
    void RotateToPlayer()
    {
        //获得一向量坐标点与另一坐标点之间夹角
        Vector3 targetDir = player.transform.position - transform.position;
        Vector3 forward = transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        //两坐标点向量与世界坐标Z轴向量之间夹角，即为要旋转到的角度
        float targetAngle = Vector3.Angle(targetDir, Vector3.forward);

        if (transform.position.x > player.transform.position.x)
        {
            targetRotation = Quaternion.Euler(0, -targetAngle, 0);
        }
        else
        {
            targetRotation = Quaternion.Euler(0, targetAngle, 0);
        }
        if (angle > 5)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 8 * Time.deltaTime);
        }
    }

    public void GetDamage(int damage)
    {
        if (HP > 0)
        {
            //弹出伤害
            HP -= damage;
            if (HP <= 0)
            {
                TaskManager.Instance.UpdateTaskData(this.gameObject.name, 1);

                //随机掉落物品
                ThrowRandomItem(itemPrefabs);


                animation.Play("death");
                //QuestManager.Instance.AddEnemy(ID);
                //销毁自己
                Destroy(gameObject, 0.5f);
            }
        }
    }

    private void ThrowRandomItem(List<GameObject> itemPrefabs)
    {
        int rNum = UnityEngine.Random.Range(0, itemPrefabs.Count - 1);

        Instantiate(itemPrefabs[rNum], transform.position, transform.rotation);

    }
}