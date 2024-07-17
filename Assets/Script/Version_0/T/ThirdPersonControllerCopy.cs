using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

namespace StarterAssets
{
    [RequireComponent(typeof(CharacterController))]
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
	[RequireComponent(typeof(PlayerInput))]
#endif
    public class ThirdPersonControllerCopy : MonoBehaviour
    {

        [Header("panel")]
        public GameObject task;
        public GameObject duihua;
        public GameObject beibao;



        [Header("Player")]
        [Tooltip("Move speed of the character in m/s")]
        public int HP = 100;
        public int MaxHP = 100;

        public float MoveSpeed = 2.0f;
        [Tooltip("Sprint speed of the character in m/s")]
        public float SprintSpeed = 5.0f;
        [Tooltip("How fast the character turns to face movement direction")]
        [Range(0.0f, 0.3f)]
        public float RotationSmoothTime = 0.12f;
        [Tooltip("Acceleration and deceleration")]
        public float SpeedChangeRate = 10.0f;

        [Space(10)]
        [Tooltip("The height the player can jump")]
        public float JumpHeight = 1.2f;
        [Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
        public float Gravity = -15.0f;

        [Space(10)]
        [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
        public float JumpTimeout = 0.50f;
        [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
        public float FallTimeout = 0.15f;

        [Header("Player Grounded")]
        [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
        public bool Grounded = true;
        [Tooltip("Useful for rough ground")]
        public float GroundedOffset = -0.14f;
        [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
        public float GroundedRadius = 0.28f;
        [Tooltip("What layers the character uses as ground")]
        public LayerMask GroundLayers;

        [Header("Cinemachine")]
        [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
        public GameObject CinemachineCameraTarget;
        [Tooltip("How far in degrees can you move the camera up")]
        public float TopClamp = 70.0f;
        [Tooltip("How far in degrees can you move the camera down")]
        public float BottomClamp = -30.0f;
        [Tooltip("Additional degress to override the camera. Useful for fine tuning camera position when locked")]
        public float CameraAngleOverride = 0.0f;
        [Tooltip("For locking the camera position on all axis")]
        public bool LockCameraPosition = false;

        // cinemachine
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        // player
        private float _speed;
        private float _animationBlend;
        private float _targetRotation = 0.0f;
        private float _rotationVelocity;
        private float _verticalVelocity;
        private float _terminalVelocity = 53.0f;

        // timeout deltatime
        private float _jumpTimeoutDelta;
        private float _fallTimeoutDelta;

        // animation IDs
        private int _animIDSpeed;
        private int _animIDGrounded;
        private int _animIDJump;
        private int _animIDFreeFall;
        private int _animIDMotionSpeed;
        private int _animIDSkill1;
        private int _animIDSkill2;
        private int _animIDSkill3;
        private int _animIDDeath;


        private Animator _animator;
        private CharacterController _controller;
        private StarterAssetsInputs _input;
        private GameObject _mainCamera;

        private const float _threshold = 0.01f;
        [Header("Combat")]
        public AudioClip audio_Attack, audio_Die;//攻击音效，死亡音效
        public GameObject fire_Point, skillEffect1, skillEffect2, skillEffect3;

        [Tooltip("剑术延迟时间")]
        public float JianShuDelayTime = 0.15f;

        private AudioSource _audioSource;

        private float _skill1DeltaTime;

        private GameObject activeObject = null;

        private void Awake()
        {
            GameInfo.SetCoin(0);
            // get a reference to our main camera
            if (_mainCamera == null)
            {
                _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
            }
        }

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();
            _input = GetComponent<StarterAssetsInputs>();
            _audioSource = GetComponent<AudioSource>();

            AssignAnimationIDs();

            _skill1DeltaTime = JianShuDelayTime;
            _jumpTimeoutDelta = JumpTimeout;
            _fallTimeoutDelta = FallTimeout;

        }

        private void Update()
        {
            SelectNPC();
            JumpAndGravity();
            GroundedCheck();
            Move();
            UseSkill1();
            UseSkill2();
            UseSkill3();
        }

        private void LateUpdate()
        {
            if(!task.activeSelf&&!beibao.activeSelf&&!duihua.activeSelf)
            {
            CameraRotation();

            }
        }

        private void AssignAnimationIDs()
        {
            _animIDSpeed = Animator.StringToHash("Speed");
            _animIDGrounded = Animator.StringToHash("Grounded");
            _animIDJump = Animator.StringToHash("Jump");
            _animIDFreeFall = Animator.StringToHash("FreeFall");
            _animIDMotionSpeed = Animator.StringToHash("MotionSpeed");
            _animIDSkill1 = Animator.StringToHash("Attack1");
            _animIDSkill2 = Animator.StringToHash("Attack2");
            _animIDSkill3 = Animator.StringToHash("Attack3");
            _animIDDeath = Animator.StringToHash("Death");
        }

        private void GroundedCheck()
        {
            // set sphere position, with offset
            Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
            Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);


            _animator.SetBool(_animIDGrounded, Grounded);

        }

        private void CameraRotation()
        {
            // if there is an input and camera position is not fixed
            if (_input.look.sqrMagnitude >= _threshold && !LockCameraPosition)
            {
                _cinemachineTargetYaw += _input.look.x * Time.deltaTime;
                _cinemachineTargetPitch += _input.look.y * Time.deltaTime;
            }

            // clamp our rotations so our values are limited 360 degrees
            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, BottomClamp, TopClamp);

            // Cinemachine will follow this target
            CinemachineCameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + CameraAngleOverride, _cinemachineTargetYaw, 0.0f);
        }

        private void Move()
        {
            // set target speed based on move speed, sprint speed and if sprint is pressed
            float targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;

            // a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

            // note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is no input, set the target speed to 0
            if (_input.move == Vector2.zero) targetSpeed = 0.0f;

            // a reference to the players current horizontal velocity
            float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

            float speedOffset = 0.1f;
            float inputMagnitude = _input.analogMovement ? _input.move.magnitude : 1f;

            // accelerate or decelerate to target speed
            if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
            {
                // creates curved result rather than a linear one giving a more organic speed change
                // note T in Lerp is clamped, so we don't need to clamp our speed
                _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

                // round speed to 3 decimal places
                _speed = Mathf.Round(_speed * 1000f) / 1000f;
            }
            else
            {
                _speed = targetSpeed;
            }
            _animationBlend = Mathf.Lerp(_animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);

            // normalise input direction
            Vector3 inputDirection = new Vector3(_input.move.x, 0.0f, _input.move.y).normalized;

            // note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
            // if there is a move input rotate player when the player is moving
            if (_input.move != Vector2.zero)
            {
                _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + _mainCamera.transform.eulerAngles.y;
                float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity, RotationSmoothTime);

                // rotate to face input direction relative to camera position
                transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
            }


            Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

            // move the player
            _controller.Move(targetDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);


            _animator.SetFloat(_animIDSpeed, _animationBlend);
            _animator.SetFloat(_animIDMotionSpeed, inputMagnitude);
        }

        private void JumpAndGravity()
        {
            if (Grounded)
            {
                // reset the fall timeout timer
                _fallTimeoutDelta = FallTimeout;

                _animator.SetBool(_animIDJump, false);
                _animator.SetBool(_animIDFreeFall, false);

                // stop our velocity dropping infinitely when grounded
                if (_verticalVelocity < 0.0f)
                {
                    _verticalVelocity = -2f;
                }

                // Jump
                if (_input.jump && _jumpTimeoutDelta <= 0.0f)
                {
                    // the square root of H * -2 * G = how much velocity needed to reach desired height
                    _verticalVelocity = Mathf.Sqrt(JumpHeight * -2f * Gravity);

                    _animator.SetBool(_animIDJump, true);
                }

                // jump timeout
                if (_jumpTimeoutDelta >= 0.0f)
                {
                    _jumpTimeoutDelta -= Time.deltaTime;
                }
            }
            else
            {
                // reset the jump timeout timer
                _jumpTimeoutDelta = JumpTimeout;

                // fall timeout
                if (_fallTimeoutDelta >= 0.0f)
                {
                    _fallTimeoutDelta -= Time.deltaTime;
                }
                else
                {
                    _animator.SetBool(_animIDFreeFall, true);
                }

                // if we are not grounded, do not jump
                _input.jump = false;
            }

            // apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
            if (_verticalVelocity < _terminalVelocity)
            {
                _verticalVelocity += Gravity * Time.deltaTime;
            }
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void OnDrawGizmosSelected()
        {
            Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
            Color transparentRed = new Color(1.0f, 0.0f, 0.0f, 0.35f);

            if (Grounded) Gizmos.color = transparentGreen;
            else Gizmos.color = transparentRed;

            // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
            Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z), GroundedRadius);
        }

        private void UseSkill3()
        {
            if (_input.skill3)
            {
                SkillController skillCtr = GameObject.Find("Skill3").transform.GetChild(0).GetComponent<SkillController>();
                if (skillCtr != null)
                {
                    skillCtr.UseSkill();
                }
                _animator.SetBool(_animIDSkill3, true);
                _input.skill3 = false;
            }
            else
            {
                _animator.SetBool(_animIDSkill3, false);
            }
        }

        private void UseSkill2()
        {
            if (_input.skill2)
            {
                SkillController skillCtr = GameObject.Find("Skill2").transform.GetChild(0).GetComponent<SkillController>();
                if (skillCtr != null)
                {
                    skillCtr.UseSkill();
                }
                _animator.SetBool(_animIDSkill2, true);
                _input.skill2 = false;
            }
            else
            {
                _animator.SetBool(_animIDSkill2, false);
            }
        }

        private void UseSkill1()
        {
            if (_input.skill1 && _skill1DeltaTime <= 0f)
            {
                SkillController skillCtr = GameObject.Find("Skill1").transform.GetChild(0).GetComponent<SkillController>();
                if (skillCtr != null)
                {
                    skillCtr.UseSkill();
                }
                _animator.SetBool(_animIDSkill1, true);
                _input.skill1 = false;
            }
            else if (_input.skill1)
            {
                _skill1DeltaTime -= Time.deltaTime;
            }
            else
            {
                _skill1DeltaTime = JianShuDelayTime;
                _animator.SetBool(_animIDSkill1, false);
            }
        }

        //动画Attack06的帧事件-播放技能特效与音效
        public void CreateSkill1Effect()
        {
            _audioSource.clip = audio_Attack;
            _audioSource.Play();
            Instantiate(skillEffect1, fire_Point.transform.position, transform.rotation);
        }

        public void CreateSkill2Effect()
        {
            _audioSource.clip = audio_Attack;
            _audioSource.Play();
            Instantiate(skillEffect2, fire_Point.transform.position, transform.rotation);
        }

        public void CreateSkill3Effect()
        {
            _audioSource.clip = audio_Attack;
            _audioSource.Play();
            Instantiate(skillEffect3, fire_Point.transform.position, transform.rotation);
        }

        private void SelectNPC()
        {
            if (_input.select)
            {
                if (activeObject != null && activeObject.activeSelf == true)
                {
                    activeObject.SetActive(false);
                }
                if (Camera.main != null)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);


                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        GameObject gobj = hit.collider.gameObject;
                        if (gobj.tag == "Enemy")
                        {
                            Transform trans = gobj.transform.Find("Selection");
                            activeObject = trans.gameObject;
                            if (activeObject.activeSelf == false)
                            {
                                activeObject.SetActive(true);
                            }
                            gameObject.transform.LookAt(gobj.transform);
                        }
                    }
                }

            }
            _input.select = false;
        }


        public void GetDamage(int attack)
        {
            //Debug.Log("xueliang:" + this.HP);
            if (this.HP - attack < 0)
            {
                _animator.SetTrigger(_animIDDeath);

                Invoke("Revive", 3f);
            }
            this.HP -= attack;

        }

        private void Revive()
        {
            Debug.Log("3s后复活");
        }
    }

}