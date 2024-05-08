using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using StarterAssets;


public class AvatarController : MonoBehaviour
{
    //最大血量值，最大魔法值，攻击力，魔法值，防御力，魔仿力
    [SerializeField] private float f_HPMax = 100, f_MPMax = 100, f_Attack = 30, f_Magic = 50, f_ATKDefense = 5, f_MGDefense = 5;
    [SerializeField] private AudioClip audio_Attack, audio_Die;//攻击音效，死亡音效
    [SerializeField] private GameObject fire_Point, skillEffectGeYuanShu, skillEffectJianShu;

    [Tooltip("剑术延迟时间")]
    public float jianShuDelayTime = 0.15f;

    [SerializeField]
    private GameObject[] skillBar;

    private GameViewController viewController;
    private Animator anim;
    private AudioSource audioSource;

    private StarterAssetsInputs input;

    private float skill1DeltaTime;
    private float f_HP, f_MP;
    private List<GameObject> activeObjects = new List<GameObject>();
    public static AvatarController instance;

    void Start()
    {
        f_HP = f_HPMax;
        f_MP = f_MPMax;
        anim = GetComponent<Animator>();
        viewController = GameObject.Find("GameManager").GetComponent<GameViewController>();

        audioSource = GetComponent<AudioSource>();
        instance = this;
        input = GetComponent<StarterAssetsInputs>();
        skill1DeltaTime = jianShuDelayTime;
    }

    void Update()
    {
        if (f_HP <= 0)
        {
            if (!viewController.panel_Die.activeSelf)
            {
                viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Die);
            }
            return;
        }

        UseJianShu();
        UseGeYuanShu();

        SelectNPC();
    }


    private void UseGeYuanShu()
    {
        if (input.skill2)
        {
            anim.SetBool("Attack2", true);
            input.skill2 = false;
            //skillBar[1].GetComponent<SkillController>().UseSkill();
        }
        else
        {
            anim.SetBool("Attack2", false);
        }
    }

    private void UseJianShu()
    {
        if (input.skill1 && skill1DeltaTime <= 0f)
        {
            anim.SetBool("Attack1", true);
            //audioSource.clip = audio_Attack;
            //audioSource.Play();
            input.skill1 = false;
            //skillBar[0].GetComponent<SkillController>().UseSkill();
        }
        else if (input.skill1)
        {
            skill1DeltaTime -= Time.deltaTime;
        }
        else
        {
            skill1DeltaTime = jianShuDelayTime;
            anim.SetBool("Attack1", false);
        }
    }

    //动画Attack06的帧事件-播放技能特效与音效
    public void CreateSkill1Effect()
    {
        audioSource.clip = audio_Attack;
        audioSource.Play();
        Instantiate(skillEffectGeYuanShu, fire_Point.transform.position, transform.rotation);
    }

    public void CreateSkill2Effect()
    {
        audioSource.clip = audio_Attack;
        audioSource.Play();
        Instantiate(skillEffectJianShu, fire_Point.transform.position, transform.rotation);
    }


    //进入碰撞检测区域
    void OnTriggerEnter(Collider other)
    {
        //被怪物攻击，更新血量
        if (other.tag == "Attack_Enemy")
        {


        }
        //到达传送门，跳转场景
        /*else if (other.tag == "JumpDoor" && GameObject.Find("GameManager").GetComponent<GameController>().Get_Task())
        {
            GameObject.Find("GameManager").GetComponent<GameController>().JumpScene();
        }*/
        //箱子，获得奖励，完成任务并记录，跳转至胜利界面
        else if (other.tag == "Box")
        {
            //viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Win);
        }
    }

    //离开碰撞区域
    void OnTriggerExit(Collider other)
    {
        //遇到NPC，答题和做任务
        if (other.tag == "NPC")
        {
            viewController.SetPanelActive(viewController.panel_GameMain, null);
        }
    }


    //更新生命
    void UpdateHP(int hp)
    {
        f_HP += hp;
        if (f_HP < 0) { f_HP = 0; }
        else if (f_HP > f_HPMax) { f_HP = f_HPMax; }
        //viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateHP(f_HP, f_HPMax);
    }

    //更新体力
    void UpdateMP(int mp)
    {
        f_MP += mp;
        if (f_MP < 0) { f_MP = 0; }
        else if (f_MP > f_MPMax) { f_MP = f_MPMax; }
        //viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateMP(f_MP, f_MPMax);
    }

    //使玩家停止移动方法
    public void CanMove(bool canMove)
    {
        if (canMove) { GetComponent<PlayerInput>().enabled = true; }
        else { GetComponent<PlayerInput>().enabled = false; }
    }

    //获得小物品
    public void GetItem(string itemName)
    {
        //更新小物品数据
        if (itemName == ConstString.item_HP)
            GameInfo.Set_Item_HP(GameInfo.Get_Item_HP() + 1);
        else if (itemName == ConstString.item_MP)
            GameInfo.Set_Item_MP(GameInfo.Get_Item_MP() + 1);
        else if (itemName == ConstString.item_Attack)
            GameInfo.Set_Item_Attack(GameInfo.Get_Item_Attack() + 1);
        else if (itemName == ConstString.item_Magic)
            GameInfo.Set_Item_Magic(GameInfo.Get_Item_Magic() + 1);
        else if (itemName == ConstString.item_ATKDefense)
            GameInfo.Set_Item_ATKDefense(GameInfo.Get_Item_ATKDefense() + 1);
        else if (itemName == ConstString.item_MGDefense)
            GameInfo.Set_Item_MGDefense(GameInfo.Get_Item_MGDefense() + 1);
        //刷新小物品界面
        //viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateItems();
    }

    //使用小物品
    public void UseItem(string itemName)
    {
        string detail = "";
        //更新小物品数据
        if (itemName == ConstString.item_HP)//增加血量
        {
            if (GameInfo.Get_Item_HP() > 0)
            {
                UpdateHP(50);
                GameInfo.Set_Item_HP(GameInfo.Get_Item_HP() - 1);
                detail = "您使用了血量包补充血量，血量+50";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_MP)//增加体力
        {
            if (GameInfo.Get_Item_MP() > 0)
            {
                UpdateMP(50);
                GameInfo.Set_Item_MP(GameInfo.Get_Item_MP() - 1);
                detail = "您使用了体力包补充体力，体力+50";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_Attack)//增加攻击力
        {
            if (GameInfo.Get_Item_Attack() > 0)
            {
                f_Attack++;
                GameInfo.Set_Item_Attack(GameInfo.Get_Item_Attack() - 1);
                detail = "您服用了伟哥包补充战斗力，战斗力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_Magic)//增加魔法值
        {
            if (GameInfo.Get_Item_Magic() > 0)
            {
                f_Magic++;
                GameInfo.Set_Item_Magic(GameInfo.Get_Item_Magic() - 1);
                detail = "您使用了超级伟哥补充魔法力，魔法力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_ATKDefense)//增加普通防御力
        {
            if (GameInfo.Get_Item_ATKDefense() > 0)
            {
                f_ATKDefense++;
                GameInfo.Set_Item_ATKDefense(GameInfo.Get_Item_ATKDefense() - 1);
                detail = "您使用了避孕套增强防御力，普通防御力+1";
            }
            else { return; }
        }
        else if (itemName == ConstString.item_MGDefense)//增加魔仿力
        {
            if (GameInfo.Get_Item_MGDefense() > 0)
            {
                f_MGDefense++;
                GameInfo.Set_Item_MGDefense(GameInfo.Get_Item_MGDefense() - 1);
                detail = "您服用了避孕药增强魔防力，魔仿力+1";
            }
            else { return; }
        }
        //viewController.SetPanelActive(viewController.panel_GameMain, viewController.panel_Detail);
        //viewController.panel_Detail.GetComponent<Panel_DetailView>().SetDetail(detail);
        //viewController.panel_GameMain.GetComponent<Panel_GameMainView>().UpdateItems();
    }

    private void SelectNPC()
    {
        if (input.select)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject gobj = hit.collider.gameObject;
                Debug.Log("Hit gobj:" + gobj.name);
                if (gobj.tag == "Enemy")
                {
                    Transform trans = gobj.transform.Find("Selection");
                    GameObject selectEffectObj = trans.gameObject;
                    if (selectEffectObj.activeSelf == false)
                    {
                        selectEffectObj.SetActive(true);
                        activeObjects.Add(selectEffectObj);
                    }
                    gameObject.transform.LookAt(gobj.transform);
                }
                else
                {
                    foreach (GameObject obj in activeObjects)
                    {
                        if (obj != null)
                        {
                            obj.SetActive(false);
                        }
                    }
                    activeObjects.Clear();
                }
            }

            input.select = false;
        }



    }

}
