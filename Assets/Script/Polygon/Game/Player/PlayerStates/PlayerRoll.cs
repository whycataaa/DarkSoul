using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    [CreateAssetMenu(menuName ="Data/Polygon/Roll",fileName ="Roll_Player")]
    public class PlayerRoll : PlayerState
    {
        [SerializeField]float MaxRollVelocity;
        private float rollTimer=0;
        Vector3 dir;
        public override void Enter()
        {
            base.Enter();

            if(CameraControl.CurrentCameraMode==CameraMode.ThirdPerson)
            {
                Vector3 targetDir;

                targetDir=playerControl.cameraTrans.forward*PlayerInputHandler.Instance.Vertical;
                targetDir+=playerControl.cameraTrans.right*PlayerInputHandler.Instance.Horizontal;

                targetDir.Normalize();
                targetDir.y=0;
                dir=targetDir;
            }
            else
            {
                Vector3 targetDir;
                targetDir=playerControl.transform.forward*PlayerInputHandler.Instance.Vertical;
                targetDir+=playerControl.transform.right*PlayerInputHandler.Instance.Horizontal;

                targetDir.Normalize();
                targetDir.y=0;
                dir=targetDir;
            }
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if(PlayerAnimationTrigger.IsPhysicRoll)
            {
                rollTimer += Time.deltaTime;
            }

            if(IsAnimationFinished)
            {
                playerStateMachine.ChangeState(typeof(PlayerIdle));
            }
        }

        public override void PhysicUpdate()
        {
            base.PhysicUpdate();

            playerControl.Rb.transform.forward=dir==Vector3.zero?playerControl.Rb.transform.forward:dir;

            Roll();
        }

        public override void Exit()
        {
            base.Exit();
            rollTimer=0;
            PlayerInputHandler.Instance.IsRoll=false;
            PlayerAnimationTrigger.IsPhysicRoll=false;
        }
        private void Roll()
        {
            if (PlayerAnimationTrigger.IsPhysicRoll)
            {
                float velocity = -MaxRollVelocity/stateDuration*rollTimer+MaxRollVelocity;
                Vector3 v = playerControl.Rb.transform.forward*velocity;
                playerControl.SetVelocity(v.x,v.y,v.z);
            }
        }
    }
}
