using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerAnimationTrigger : MonoBehaviour
    {
        public static bool IsPhysicRoll=false;


        //开始真正翻滚
        public void PhysicStartRoll()
        {
            IsPhysicRoll=true;
        }
        public void PhysicEndRoll()
        {
            IsPhysicRoll=false;
        }

    }
}
