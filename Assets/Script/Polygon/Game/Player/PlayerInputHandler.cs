using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PolygonProject
{
    public class PlayerInputHandler : SingletonMono<PlayerInputHandler>
    {
        #region Move
        //按下翻滚键多久进入跑步
        [SerializeField]float RUN_HOLD_TIME=0.5f;
        bool IsRunTimerStart=false;
        public float Horizontal;
        public float Vertical;
        public float MoveAmount=>Mathf.Clamp01(Mathf.Abs(Horizontal)+Mathf.Abs(Vertical));
        public bool IsRoll=false;
        public bool IsJump=>inputActions.GamePlay.Jump.WasReleasedThisFrame();
        public bool IsSprint=false;
        public bool IsSwitchCameraMode=>inputActions.GamePlay.Lock.WasPressedThisFrame();
        public bool IsBag=>inputActions.GamePlay.Bag.WasPressedThisFrame();
        PlayerInputAsset inputActions;

        #endregion

        #region Battle
        public bool IsAttack=>inputActions.GamePlay.LMouse.WasPressedThisFrame();
        public bool IsDefense=>inputActions.GamePlay.RMouse.WasPressedThisFrame();

        public bool IsSwitchLWeapon=>inputActions.GamePlay.SwitchLWeapon.WasPressedThisFrame();
        public bool IsSwitchRWeapon=>inputActions.GamePlay.SwitchRWeapon.WasPressedThisFrame();
        public bool IsSwitchUpItem=>inputActions.GamePlay.SwitchUpItem.WasPressedThisFrame();
        public bool IsSwitchDownItem=>inputActions.GamePlay.SwitchDownItem.WasPressedThisFrame();
        #endregion
        #region UI
        public bool IsShowCursor;
        public bool IsRightMouse=>inputActions.UI.RightClick.WasPressedThisFrame();
        #endregion

        [SerializeField]Vector2 movementInput
        {
            set
            {
                Horizontal=value.x;
                Vertical=value.y;
            }
        }

        public float runTimer;
        public void OnEnable()
        {
            if(inputActions==null)
            {
                inputActions=new();
                inputActions.GamePlay.Move.performed+=inputActions=>movementInput=inputActions.ReadValue<Vector2>();
                inputActions.GamePlay.Move.canceled+=inputActions=>movementInput=Vector2.zero;



                //按住按键鼠标显示
                inputActions.GamePlay.CursorShow.performed+=i=>IsShowCursor=true;
                inputActions.GamePlay.CursorShow.canceled+=i=>IsShowCursor=false;

                //按下翻滚检测是否按住，如果按住就跑步，松开直接翻滚
                inputActions.GamePlay.Roll.started+=OnRollStarted;
                inputActions.GamePlay.Roll.canceled+=OnRollCanceled;



            }

            inputActions.Enable();
        }

        private void OnRollCanceled(InputAction.CallbackContext context)
        {
            IsRunTimerStart=false;
            IsRoll=true;
            if(IsSprint)
            {
                IsSprint=false;
                IsRoll=false;
            }

        }

        private void OnRollStarted(InputAction.CallbackContext context)
        {
            runTimer=RUN_HOLD_TIME;
            IsRunTimerStart=true;
        }



        private void Update()
        {
            if(IsRunTimerStart)
            {
                runTimer-=Time.deltaTime;
                if(runTimer<=0)
                {
                    IsSprint=true;
                    IsRunTimerStart=false;
                }
            }


        }
        public void OnDisable()
        {
            inputActions.Disable();
        }


        public void EnableUI()
        {
            inputActions.GamePlay.Disable();
            inputActions.UI.Enable();
        }

        public void DisableUI()
        {
            inputActions.UI.Disable();
            inputActions.GamePlay.Enable();
        }
    }
}
