using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
/// <summary>
/// 武器管理类，管理每个武器以及攻击判定
/// </summary>
public class WeaponManager : MonoBehaviour
{
    //当前武器序号
    [SerializeField]private int currentWeaponIndex=0;
    //最大武器数量
    private int maxWeaponCount;
    //当前装备的武器
    [SerializeField]private Weapon currentWeapon;
    //绑定武器的位置
    public Transform weaponTrans;
    //是否在检测
    [SerializeField]public bool OnDetect=false;
    //武器列表
    [SerializeField]List<Weapon> weapons=new List<Weapon>();
    //攻击检测的列表
    [SerializeField]List<Detection> detections=new List<Detection>();
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        weaponTrans=TransformHelper.FindDeepTransform<Transform>(transform,"WeaponTrans");
        maxWeaponCount=weapons.Count;


    }
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        SwitchWeapon(currentWeaponIndex);
    }
    public WeaponType GetCurrentWeaponType()
    {
        return weapons[currentWeaponIndex].weaponType;
    }
    /// <summary>
    /// 根据当前的武器返回武器的攻击信息
    /// </summary>
    /// <param name="currentWeapon"></param>
    /// <returns></returns>
    AttackInfo GetCurrentAttackInfo(Weapon currentWeapon)
    {
        AttackInfo info=new AttackInfo(currentWeapon.baseAttack,currentWeapon.weaponType);
        return info;
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(detections.Count>0)
        {
            HandleDetection(OnDetect);
        }
    }
    /// <summary>
    /// 切换武器
    /// </summary>
    public void SwitchWeapon()
    {
        currentWeaponIndex++;
        currentWeaponIndex%=maxWeaponCount;


    }
    /// <summary>
    /// 根据标签切换武器
    /// </summary>
    /// <param name="weaponIndex"></param>
    public void SwitchWeapon(int weaponIndex)
    {
        //先清楚攻击检测
        detections.Clear();
        //销毁武器
        if(currentWeapon!=null)
        {
            Destroy(weaponTrans.GetChild(0).gameObject);
        }
        //新增武器
        currentWeaponIndex=weaponIndex;
        currentWeapon=weapons[currentWeaponIndex];
        Instantiate(currentWeapon.weaponPrefab,weaponTrans);
        detections.Add(weaponTrans.GetComponentInChildren<Detection>());
    }

    /// <summary>
    /// 执行攻击检测
    /// </summary>
    void AttackDetection()
    {
        if(OnDetect)
        {
            foreach(var detection in detections)
            {
                foreach(var hit in detection.GetDetection())
                {
                    hit.GetComponent<AgentHitBox>().GetDamage(GetCurrentAttackInfo(currentWeapon));
                }
            }
        }
    }

    public void HandleDetection(bool value)
    {
        if(value)
        {
            AttackDetection();
        }
        else
        {
            foreach(var detection in detections)
            {
                detection.ClearWasHit();
            }
        }
    }
}
}