using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : Singleton<WeaponManager>
{
    [SerializeField]List<Weapon> weapons=new List<Weapon>();
    private int currentWeaponID=0;
    private int maxWeaponCount;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        currentWeaponID=weapons[0].WeaponID;
    }
    public WeaponType GetCurrentWeaponType()
    {
        return weapons[currentWeaponID].weaponType;
    }

}
