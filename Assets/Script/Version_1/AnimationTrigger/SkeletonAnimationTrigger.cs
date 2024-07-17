using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace game2{
public class SkeletonAnimationTrigger : EnemyAnimationTrigger
{
    public Skeleton skeleton;
    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
    {
        player=GameObject.Find("PlayerControl/Player");
        skeleton=GetComponentInParent<Skeleton>();
    }
}
}