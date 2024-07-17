using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerAnimatorHandler : MonoBehaviour
    {
        public Animator animator;
        int vertical;
        int horizontal;
        public bool canRotate;

        public void Init()
        {
            canRotate=true;
            animator=GetComponent<Animator>();
        }

        public void UpdateAnimationValues(float verticalMovement,float horizontalMovement)
        {
            #region 前后
            float v;
            if (verticalMovement>0&&verticalMovement<0.55f)
            {
                v=0.5f;
            }
            else if(verticalMovement>0.55f)
            {
                v=1;
            }
            else if(verticalMovement<0&&verticalMovement>-0.55f)
            {
                v=-0.5f;
            }
            else if(verticalMovement<-0.55f)
            {
                v=-1;
            }
            else
            {
                v=0;
            }
            #endregion

            #region 左右
            float h;
            if (horizontalMovement>0&&horizontalMovement<0.55f)
            {
                h=0.5f;
            }
            else if(horizontalMovement>0.55f)
            {
                h=1;
            }
            else if(horizontalMovement<0&&horizontalMovement>-0.55f)
            {
                h=-0.5f;
            }
            else if(horizontalMovement<-0.55f)
            {
                h=-1;
            }
            else
            {
                h=0;
            }
            #endregion

            animator.SetFloat(vertical,v,0.1f,Time.deltaTime);
            animator.SetFloat(horizontal,h,0.1f,Time.deltaTime);
        }

        public void HandleRotate(bool _canRotate)
        {
            canRotate=_canRotate;
        }
    }
}
