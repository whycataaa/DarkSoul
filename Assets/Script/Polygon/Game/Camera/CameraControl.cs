using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace PolygonProject
{
    public enum CameraMode
    {
        //第三人称视角
        ThirdPerson,
        //锁定视角
        Locked
    }
    /// <summary>
    /// 基于CinemachineVirtualCamera的相机控制
    /// </summary>
    public class CameraControl : MonoBehaviour
    {
        public static CameraMode CurrentCameraMode;
        //索敌距离
        public float LockRadius=10;
        //当前的虚拟相机
        private CinemachineVirtualCamera virtualCamera3rd;
        private CinemachineVirtualCamera virtualCameraLock;
        
        public Vector3 CurrentLockedPos;
        void Awake()
        {
            virtualCamera3rd=GetComponent<CinemachineVirtualCamera>();
            virtualCameraLock=GameObject.Find("PlayerCameraLock").GetComponent<CinemachineVirtualCamera>();
        }


        public void SetCameraAim(Transform _aimTrans,GameObject camera)
        {
            camera.GetComponent<CinemachineVirtualCamera>().LookAt=_aimTrans;
        }

        public void SetCameraFollow(Transform _followTrans,GameObject camera)
        {
            camera.GetComponent<CinemachineVirtualCamera>().Follow=_followTrans;
        }

        public void SwitchCameraMode(CameraMode cameraMode,Vector3 pos)
        {
            CurrentCameraMode=cameraMode;
            switch(cameraMode)
            {
                case CameraMode.ThirdPerson:

                virtualCamera3rd.gameObject.SetActive(true);
                virtualCameraLock.gameObject.SetActive(false);

                virtualCamera3rd.transform.position=Vector3.zero;

                    break;
                case CameraMode.Locked:
                    Collider[] cd=new Collider[100];
                    int cdNum=Physics.OverlapSphereNonAlloc(pos,LockRadius,cd,LayerMask.GetMask("Enemy"));
                    Debug.Log(cdNum);
                    float[] dis=new float[cdNum];
                    if(cdNum==0)
                    {
                        SwitchCameraMode(CameraMode.ThirdPerson,Vector3.zero);
                    }
                    else
                    {
                        int nearestEnemy=0;
                        for(int i=0;i<cdNum;i++)
                        {
                            dis[i]=(cd[i].transform.position-pos).sqrMagnitude;
                            if(dis[i]>dis[nearestEnemy])
                            {
                                nearestEnemy=i;
                            }
                        }
                        virtualCameraLock.LookAt=TransformHelper.FindDeepTransform<Transform>(cd[nearestEnemy].transform,"LockPoint");
                        virtualCameraLock.gameObject.SetActive(true);
                        virtualCamera3rd.gameObject.SetActive(false);
                        CurrentLockedPos=cd[nearestEnemy].transform.position;
                    }

                    break;
            }
        }

        public Vector3 GetLockedPos()
        {
            return CurrentLockedPos;
        }
    }
}
