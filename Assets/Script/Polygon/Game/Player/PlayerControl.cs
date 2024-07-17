using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace PolygonProject
{

    public class PlayerControl : MonoBehaviour
    {


        //相机
        public Transform cameraTrans;
        CameraControl cameraControl;
        //动画
        PlayerAnimatorHandler animatorHandler;
        Animator animator;
        //状态机
        public PlayerStateMachine playerStateMachine;
        Vector3 moveDirection;

        private Transform characterTrans;
        public Rigidbody Rb;
        public GroundDetector groundDetector;
        public bool IsGround=>groundDetector.IsGround;

        public WeaponManager playerWeaponManager;
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        void Awake()
        {
            cameraControl=GameObject.Find("PlayerCamera").GetComponent<CameraControl>();
            animator=GetComponentInChildren<Animator>();
            Rb=GetComponent<Rigidbody>();
            animatorHandler=GetComponentInChildren<PlayerAnimatorHandler>();
            groundDetector=GetComponent<GroundDetector>();
            cameraTrans=Camera.main.transform;
            characterTrans=transform;
            playerStateMachine=GetComponent<PlayerStateMachine>();
            playerStateMachine.Init();
            cameraControl.SwitchCameraMode(CameraMode.ThirdPerson,gameObject.transform.position);
        }
        void Start()
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }


        void Update()
        {
            playerStateMachine.LogicUpdate();
            if(PlayerInputHandler.Instance.IsSwitchCameraMode)
            {
                cameraControl.SwitchCameraMode(CameraControl.CurrentCameraMode==
                                             CameraMode.ThirdPerson?CameraMode.Locked:CameraMode.ThirdPerson
                                            ,transform.position);


            }
            if(CameraControl.CurrentCameraMode==CameraMode.Locked)
            {
                animator.SetFloat("Horizontal",PlayerInputHandler.Instance.Horizontal==0?0:Mathf.Sign(PlayerInputHandler.Instance.Horizontal),0.2f,Time.deltaTime);
                animator.SetFloat("Vertical",PlayerInputHandler.Instance.Vertical==0?0:Mathf.Sign(PlayerInputHandler.Instance.Vertical),0.2f,Time.deltaTime);
            }
        }
        void FixedUpdate()
        {
            playerStateMachine.PhysicUpdate();
        }

        /// <summary>
        /// This function is called when the MonoBehaviour will be destroyed.
        /// </summary>
        void OnDestroy()
        {

        }
        public void Move(float _moveSpeed,float _rotationSpeed)
        {
            switch(CameraControl.CurrentCameraMode)
            {
                case CameraMode.ThirdPerson:

                // 计算相机的前向和右向，并移除其y分量
                Vector3 forward = cameraTrans.forward;
                Vector3 right = cameraTrans.right;
                forward.y=0;
                right.y=0;
                moveDirection = forward * PlayerInputHandler.Instance.Vertical + right * PlayerInputHandler.Instance.Horizontal;
                moveDirection.Normalize();

                Rb.velocity=moveDirection*_moveSpeed;

                HandleRotation(_rotationSpeed);
                break;
                case CameraMode.Locked:
                animator.Play("LockMove");

                HandleRotation(0);

                moveDirection =Rb.transform.forward * PlayerInputHandler.Instance.Vertical + Rb.transform.right * PlayerInputHandler.Instance.Horizontal;
                moveDirection.Normalize();
                Rb.velocity=moveDirection*_moveSpeed;



                break;
            }

        }


        public void HandleRotation(float _rotationSpeed)
        {
            switch(CameraControl.CurrentCameraMode)
            {
                case CameraMode.ThirdPerson:
                Vector3 targetDir;

                targetDir=cameraTrans.forward*PlayerInputHandler.Instance.Vertical;
                targetDir+=cameraTrans.right*PlayerInputHandler.Instance.Horizontal;

                targetDir.Normalize();
                targetDir.y=0;


                Quaternion quaternion=Quaternion.LookRotation(targetDir);
                Quaternion targetRotation=Quaternion.Slerp(characterTrans.rotation,quaternion,_rotationSpeed*Time.deltaTime);

                characterTrans.rotation=targetRotation;
                break;

                case CameraMode.Locked:
                var ePos=cameraControl.GetLockedPos();
                Vector3 dir =ePos-Rb.transform.position;
                dir.y=0;
                Rb.transform.forward=dir;
                break;
            }

        }

        /// <summary>
        /// 设置玩家速度为0
        /// </summary>
        public void SetZeroVelocity()
        {
            Rb.velocity=Vector3.zero;
        }

        public void SetVelocityY(float velocityY)
        {
            Rb.velocity=new Vector3(Rb.velocity.x,velocityY,Rb.velocity.z);
        }
        public void SetVelocity(float velocityX,float velocityY,float velocityZ)
        {
            Rb.velocity=new Vector3(velocityX,velocityY,velocityZ);
        }

    }
}
