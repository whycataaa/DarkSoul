using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
public class ReaperAnimationTrigger : EnemyAnimationTrigger
{
    public Reaper reaper;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        player=GameObject.Find("PlayerControl/Player");
        reaper=GetComponentInParent<Reaper>();
    }
    public void CreateWave()
    {
        reaper.CreateWave();
    }
    public void Shoot()
    {
        reaper.Shoot();
    }
}
}