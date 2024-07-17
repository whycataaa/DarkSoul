using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PolygonProject
{
    public class PlayerStateMachine : StateMachine
    {
        public PlayerState[] playerStates;
        public PlayerControl playerControl;
        public Animator animator;
        public PlayerInputHandler playerInputHandler;



        public void Init()
        {
            playerInputHandler=PlayerInputHandler.Instance;
            playerControl=GetComponent<PlayerControl>();
            animator=GetComponentInChildren<Animator>();
            stateTable=new Dictionary<System.Type, IState>(playerStates.Length);
            //遍历状态并进行初始化
            foreach(var playerState in playerStates)
            {
                playerState.Initialize(animator,this,playerControl);
                stateTable.Add(playerState.GetType(),playerState);
            }
            currentState=playerStates[0];
        }
    }
}
