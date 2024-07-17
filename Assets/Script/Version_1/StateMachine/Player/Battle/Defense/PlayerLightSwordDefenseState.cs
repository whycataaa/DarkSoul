using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using game2;
namespace game2{
/// <summary>
/// 轻剑防御状态
/// </summary>
[CreateAssetMenu(menuName ="Data/StateMachine/PlayerState/Battle/LightSwordDefense_Player",fileName ="LightSwordDefense_Player")]
public class PlayerLightSwordDefenseState : PlayerBattleState
{
    [Header("前后左右动画名")]
    [SerializeField]string[] animName;
    protected Vector2 MoveVector2=>PlayerInput.Instance.MoveVector2;
    protected bool canWalk=>PlayerInput.Instance.IsWalk;
    /// <summary>
    /// 基础速度
    /// </summary>
    [SerializeField]private float BaseSpeed=4;
    /// <summary>
    /// 改变速度的倍率
    /// </summary>
    [SerializeField][Range(0,5)]protected float movementSpeedModifier=1;

    //当前的旋转
    protected Vector3 currentTargetRotation;
    //旋转速度
    protected Vector3 dampedTargetRotationCurrentVelocity;
    //旋转时间
    protected Vector3 dampedTargetRotationPassedTime;
    protected float movementOnSlopesSpeedModifier=1;
    public override void Enter()
    {
        base.Enter();
        playerController.CanRecoverStamina=true;
    }

    public override void LogicUpdate()
    {
        if(PlayerInput.Instance.IsAttack)
        {
            playerStateMachine.ChangeState(typeof(PlayerLightSwordParryState));
        }
        if(!PlayerInput.Instance.IsDefense)
        {
            playerStateMachine.ChangeState(typeof(PlayerIdle));
        }

        if(MoveVector2!=Vector2.zero)
        {
            animator.Play(animName[0]);
        }
        else
        {
            animator.CrossFade(stateName,0.1f);
        }

    }
    public override void HandleAttackReceived(AttackInfo info)
    {

        if(info.weaponType<=WeaponType.Light)
        {
            if(playerController.CurrentStamina<=0)
            {
                playerStateMachine.ChangeState(typeof(PlayerLightSwordBrokenHit));
            }
            else
            {
                playerStateMachine.ChangeState(typeof(PlayerLightSwordDefenseLightHitState));
            }
        }
    }

    public override void PhysicUpdate()
    {
        Move();
    }
    public override void Exit()
    {
        base.Exit();
    }
    private void Move()
    {

        if (MoveVector2 == Vector2.zero || movementSpeedModifier== 0f)
            {
                return;
            }
            //先获取移动的方向
            Vector3 movementDirection = GetMovementInputDirection();
            //要绕y轴旋转的角度
            float targetRotationYAngle = Rotate(movementDirection);

            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            //获取移动速度
            float movementSpeed = GetMovementSpeed();
            //当前的水平速度
            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();

            playerStateMachine.playerController.rb.AddForce(targetRotationDirection * movementSpeed - currentPlayerHorizontalVelocity, ForceMode.VelocityChange);
    }
    protected Vector3 GetMovementInputDirection()
    {
        return new Vector3(MoveVector2.x, 0f, MoveVector2.y);
    }
    private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);

            RotateTowardsTargetRotation();

            return directionAngle;
        }
    protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotation = true)
    {
        float directionAngle = GetDirectionAngle(direction);

        if (shouldConsiderCameraRotation)
        {
            directionAngle = AddCameraRotationToAngle(directionAngle);
        }

        if (directionAngle != currentTargetRotation.y)
        {
            UpdateTargetRotationData(directionAngle);
        }

        return directionAngle;
    }
    private float GetDirectionAngle(Vector3 direction)
    {
        float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        if (directionAngle < 0f)
        {
            directionAngle += 360f;
        }

        return directionAngle;
    }
    private float AddCameraRotationToAngle(float angle)
        {
            angle += playerStateMachine.playerController.playerCameraTrans.eulerAngles.y;

            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }
        protected float GetMovementSpeed(bool shouldConsiderSlopes = false)
        {
            float movementSpeed = BaseSpeed * movementSpeedModifier;

            if (shouldConsiderSlopes)
            {
                movementSpeed *= movementOnSlopesSpeedModifier;
            }

            return movementSpeed;
        }
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = playerStateMachine.playerController.rb.velocity;

            playerHorizontalVelocity.y = 0f;

            return playerHorizontalVelocity;
        }
        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = playerStateMachine.playerController.rb.rotation.eulerAngles.y;

            if (currentYAngle == currentTargetRotation.y)
            {
                return;
            }

            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y,0.14f - dampedTargetRotationPassedTime.y);

            dampedTargetRotationPassedTime.y += Time.deltaTime;

            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);

            playerStateMachine.playerController.rb.MoveRotation(targetRotation);
        }
        protected Vector3 GetTargetRotationDirection(float targetRotationAngle)
        {
            return Quaternion.Euler(0f, targetRotationAngle, 0f) * Vector3.forward;
        }
        private void UpdateTargetRotationData(float targetAngle)
        {
            currentTargetRotation.y = targetAngle;

            dampedTargetRotationPassedTime.y = 0f;
        }

}
}