using System;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using DG.Tweening;

namespace VHS
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Variables
            #region Private Serialized     
                #region Data
                    [Space, Header("Data")]
                    [SerializeField] private HeadBobData headBobData = null;

                #endregion
                    
                #region Locomotion
                    [Space, Header("Locomotion Settings")]
                    [SerializeField] private float crouchSpeed = 1f;
                    [SerializeField] private float walkSpeed = 2f;
                    [SerializeField] private float runSpeed = 3f;
                    [SerializeField] private float jumpSpeed = 5f;
                    [SerializeField] private float slideSpeed = 7f;

                    [Space]

                    [Slider(0f,1f)][SerializeField] private float moveBackwardsSpeedPercent = 0.5f;
                    [Slider(0f,1f)][SerializeField] private float moveSideSpeedPercent = 0.75f;
                #endregion

                #region Run Settings
                    [Space, Header("Run Settings")]
                    [Slider(-1f,1f)][SerializeField] private float canRunThreshold = 0.8f;
                    [SerializeField] private AnimationCurve runTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Crouch Settings
                    [Space, Header("Crouch Settings")]
                    [Slider(0.2f,0.9f)][SerializeField] private float crouchPercent = 0.6f;
                    [SerializeField] private float crouchTransitionDuration = 1f;
                    [SerializeField] private AnimationCurve crouchTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region  Slide Settings
                    [Space, Header("Slide Settings")]
                    [Slider(0.2f,0.9f)][SerializeField] private float slidePercent = 0.6f;
                    [SerializeField] private float slideTransitionDuration = 1f;
                    [SerializeField] private float maxSlideDuration = 2f;
                    [SerializeField] private AnimationCurve slideTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Landing Settings
                    [Space, Header("Landing Settings")]
                    [Slider(0.05f,0.5f)][SerializeField] private float lowLandAmount = 0.1f;
                    [Slider(0.2f,0.9f)][SerializeField] private float highLandAmount = 0.6f;
                    [SerializeField] private float landTimer = 0.5f;
                    [SerializeField] private float landDuration = 1f;
                    [SerializeField] private AnimationCurve landCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Gravity
                    [Space, Header("Gravity Settings")]
                    [SerializeField] private float gravityMultiplier = 2.5f;
                    [SerializeField] private float stickToGroundForce = 5f;
                    
                    [SerializeField] private LayerMask groundLayer = ~0;
                    [Slider(0f,1f)][SerializeField] private float rayLength = 0.1f;
                    [Slider(0.01f,1f)][SerializeField] private float raySphereRadius = 0.1f;
                #endregion

                #region Wall Settings
                    [Space, Header("Check Wall Settings")]
                    [SerializeField] private LayerMask obstacleLayers = ~0;
                    [Slider(0f,1f)][SerializeField] private float rayObstacleLength = 0.1f;
                    [Slider(0.01f,1f)][SerializeField] private float rayObstacleSphereRadius = 0.1f;
                    
                #endregion

                #region Smooth Settings
                    [Space, Header("Smooth Settings")]                
                    [Range(1f,100f)] [SerializeField] private float smoothRotateSpeed = 5f;
                    [Range(1f,100f)] [SerializeField] private float smoothInputSpeed = 5f;
                    [Range(1f,100f)] [SerializeField] private float smoothVelocitySpeed = 5f;
                    [Range(1f,100f)] [SerializeField] private float smoothFinalDirectionSpeed = 5f;
                    [Range(1f,100f)] [SerializeField] private float smoothHeadBobSpeed = 5f;

                    [Space]
                    [SerializeField] private bool experimental = false;
                    [InfoBox("It should smooth our player movement to not start fast and not stop fast but it's somehow jerky", InfoBoxType.Warning)]
                    [Tooltip("If set to very high it will stop player immediately after releasing input, otherwise it just another smoothing to our movement to make our player not move fast immediately and not stop immediately")]
                    [ShowIf("experimental")] [Range(1f,100f)] [SerializeField] private float smoothInputMagnitudeSpeed = 5f;
                    
                #endregion
            #endregion
            #region Private Non-Serialized
                #region Components / Custom Classes / Caches
                    private CharacterController m_characterController;
                    private Transform m_yawTransform;
                    private Transform m_camTransform;
                    private HeadBob m_headBob;
                    private CameraController m_cameraController;
                    
                    private RaycastHit m_hitInfo;
                    private IEnumerator m_LandRoutine;
                #endregion

                #region Debug
                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector2 m_inputVector;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector2 m_smoothInputVector;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_finalMoveDir;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_smoothFinalMoveDir;
                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_finalMoveVector;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_currentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_smoothCurrentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_finalSmoothCurrentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_walkRunSpeedDifference;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_finalRayLength;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_hitWall;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_isCrouching;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_isSliding;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_isRunning;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_isGrounded;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_previouslyGrounded;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_initHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_crouchHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_slideHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_initCenter;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_crouchCenter;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private Vector3 m_slideCenter;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_initCamHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_crouchCamHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_slideCamHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_crouchStandHeightDifference;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_slideStandHeightDifference;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_duringCrouchAnimation;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_duringRunAnimation;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private bool m_duringSlideAnimation;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] private float m_inAirTimer;

                    [Space]
                    [BoxGroup("DEBUG")][ShowIf("experimental")][SerializeField][ReadOnly] private float m_inputVectorMagnitude;
                    [BoxGroup("DEBUG")][ShowIf("experimental")][SerializeField][ReadOnly] private float m_smoothInputVectorMagnitude;
                #endregion
            #endregion
        
        #endregion

        #region BuiltIn Methods     
            protected virtual void Start()
            {
                GetComponents();
                InitVariables();
            }

            protected virtual void OnEnable()
            {
                InputManager._OnPlayerSprintPressed += _OnPlayerSprintPressed;
                InputManager._OnPlayerSprintReleased += _OnPlayerSprintReleased;

                InputManager._OnPlayerCrouchPressed += _OnPlayerCrouchPressed;
                InputManager._OnPlayerCrouchReleased += _OnPlayerCrouchReleased;

                InputManager._OnPlayerJumpPressed += _OnPlayerJumpPressed;
            }

            protected virtual void OnDisable()
            {
                InputManager._OnPlayerSprintPressed -= _OnPlayerSprintPressed;
                InputManager._OnPlayerSprintReleased -= _OnPlayerSprintReleased;

                InputManager._OnPlayerCrouchPressed -= _OnPlayerCrouchPressed;
                InputManager._OnPlayerCrouchReleased -= _OnPlayerCrouchReleased;

                InputManager._OnPlayerJumpPressed -= _OnPlayerJumpPressed;
            }

            protected virtual void Update()
            {
                if(m_yawTransform != null)
                    RotateTowardsCamera();

                if(m_characterController)
                {
                    // Check if Grounded,Wall etc
                    CheckIfGrounded();
                    CheckIfWall();

                    // Apply Smoothing
                    SmoothInput();
                    SmoothSpeed();
                    SmoothDir();

                    if(experimental)
                        SmoothInputMagnitude();

                    // Calculate Movement
                    CalculateMovementDirection();
                    CalculateSpeed();
                    CalculateFinalMovement();

                    // Handle Player Movement, Gravity, Jump etc.
                    HandleHeadBob();
                    HandleRunFOV();
                    HandleCameraSway();
                    HandleLanding();

                    ApplyGravity();
                    ApplyMovement();

                    m_previouslyGrounded = m_isGrounded;
                }
            }

            /*
                private void OnDrawGizmos()
                {
                    Gizmos.color = Color.yellow;
                    Gizmos.DrawWireSphere((transform.position + m_characterController.center) - Vector3.up * m_finalRayLength, raySphereRadius);
                }
            
            */
            
        #endregion

        #region Event Callback Methods
            
            private void _OnPlayerSprintPressed()
            {
                m_isRunning = true;
                ChangeToRunFOV();
            }

            private void _OnPlayerSprintReleased()
            {
                m_isRunning = false;
                ChangeToInitFOV();
            }

            private void _OnPlayerCrouchPressed() => HandleCrouchInput();

            private void _OnPlayerCrouchReleased() => ReturnToInitHeight();

            private void _OnPlayerJumpPressed() => HandleJump();

        #endregion

        #region Custom Methods
            #region Initialize Methods    
                protected virtual void GetComponents()
                {
                    m_characterController = GetComponent<CharacterController>();
                    m_cameraController = GetComponentInChildren<CameraController>();
                    m_yawTransform = m_cameraController.transform;
                    m_camTransform = GetComponentInChildren<Camera>().transform;
                    m_headBob = new HeadBob(headBobData, moveBackwardsSpeedPercent, moveSideSpeedPercent);
                }

                protected virtual void InitVariables()
                {   
                    // Calculate where our character center should be based on height and skin width
                    m_characterController.center = new Vector3(0f,m_characterController.height / 2f + m_characterController.skinWidth,0f);

                    m_initCenter = m_characterController.center;
                    m_initHeight = m_characterController.height;
                    m_initCamHeight = m_yawTransform.localPosition.y;

                    m_crouchHeight = m_initHeight * crouchPercent;
                    m_crouchCenter = (m_crouchHeight / 2f + m_characterController.skinWidth) * Vector3.up;
                    m_crouchStandHeightDifference = m_initHeight - m_crouchHeight;
                    m_crouchCamHeight = m_initCamHeight - m_crouchStandHeightDifference;

                    m_slideHeight = m_initHeight * slidePercent;
                    m_slideCenter = (m_slideHeight / 2f + m_characterController.skinWidth) * Vector3.up;
                    m_slideStandHeightDifference = m_initHeight - m_slideHeight;
                    m_slideCamHeight = m_initCamHeight - m_slideStandHeightDifference;

                    // Sphere radius not included. If you want it to be included just decrease by sphere radius at the end of this equation
                    m_finalRayLength = rayLength + m_characterController.center.y;

                    m_isGrounded = true;
                    m_previouslyGrounded = true;

                    m_inAirTimer = 0f;
                    m_headBob.CurrentStateHeight = m_initCamHeight;

                    m_walkRunSpeedDifference = runSpeed - walkSpeed;
                }
            #endregion

            #region Smoothing Methods
                protected virtual void SmoothInput()
                {
                    m_inputVector = InputManager._MovementAxis;
                    m_smoothInputVector = Vector2.Lerp(m_smoothInputVector,m_inputVector,Time.deltaTime * smoothInputSpeed);
                    //Debug.DrawRay(transform.position, new Vector3(m_smoothInputVector.x,0f,m_smoothInputVector.y), Color.green);
                }

                protected virtual void SmoothSpeed()
                {
                    m_smoothCurrentSpeed = Mathf.Lerp(m_smoothCurrentSpeed, m_currentSpeed, Time.deltaTime * smoothVelocitySpeed);

                    /* WALK TO RUN SPEED TRANSITION || COMMENTED BECAUSE RETURNING FROM SLIDING TO RUNNING IS TOO SNAPPY
                    if(m_isRunning && CanRun() && !m_isSliding)
                    {
                        float _walkRunPercent = Mathf.InverseLerp(walkSpeed,runSpeed, m_smoothCurrentSpeed);
                        m_finalSmoothCurrentSpeed = runTransitionCurve.Evaluate(_walkRunPercent) * m_walkRunSpeedDifference + walkSpeed;
                        return;
                    }
                    */
                    
                    m_finalSmoothCurrentSpeed = m_smoothCurrentSpeed;
                }

                protected virtual void SmoothDir()
                {

                    m_smoothFinalMoveDir = Vector3.Lerp(m_smoothFinalMoveDir, m_finalMoveDir, Time.deltaTime * smoothFinalDirectionSpeed);
                    Debug.DrawRay(transform.position, m_smoothFinalMoveDir, Color.yellow);
                }
                
                protected virtual void SmoothInputMagnitude()
                {
                    m_inputVectorMagnitude = m_inputVector.magnitude;
                    m_smoothInputVectorMagnitude = Mathf.Lerp(m_smoothInputVectorMagnitude, m_inputVectorMagnitude, Time.deltaTime * smoothInputMagnitudeSpeed);
                }
            #endregion

            #region Locomotion Calculation Methods
                protected virtual void CheckIfGrounded()
                {
                    Vector3 _origin = transform.position + m_characterController.center;

                    bool _hitGround = Physics.SphereCast(_origin,raySphereRadius,Vector3.down,out m_hitInfo,m_finalRayLength,groundLayer);
                    Debug.DrawRay(_origin,Vector3.down * (m_finalRayLength),Color.red);

                    m_isGrounded = _hitGround ? true : false;
                }

                protected virtual void CheckIfWall()
                {
                    
                    Vector3 _origin = transform.position + m_characterController.center;
                    RaycastHit _wallInfo;

                    bool _hitWall = false;

                    if(InputManager._MovementAxis != Vector2.zero && m_finalMoveDir.sqrMagnitude > 0)
                        _hitWall = Physics.SphereCast(_origin,rayObstacleSphereRadius,m_finalMoveDir, out _wallInfo,rayObstacleLength,obstacleLayers);
                    Debug.DrawRay(_origin,m_finalMoveDir * rayObstacleLength,Color.blue);

                    m_hitWall = _hitWall ? true : false;
                }

                protected virtual bool CheckIfRoof() /// TO FIX
                {
                    Vector3 _origin = transform.position;
                    RaycastHit _roofInfo;

                    bool _hitRoof = Physics.SphereCast(_origin,raySphereRadius,Vector3.up,out _roofInfo,m_initHeight);

                    return _hitRoof;
                }

                protected virtual bool CanRun()
                {
                    Vector3 _normalizedDir = Vector3.zero;

                    if(m_smoothFinalMoveDir != Vector3.zero)
                        _normalizedDir = m_smoothFinalMoveDir.normalized;

                    float _dot = Vector3.Dot(transform.forward,_normalizedDir);
                    return _dot >= canRunThreshold && !m_isCrouching ? true : false;
                }

                protected virtual bool CanJump()
                {
                    return !m_isSliding && !m_isCrouching && m_characterController.isGrounded;
                }

                protected virtual bool CanCrouch()
                {
                    return m_characterController.isGrounded;
                }

                protected virtual void CalculateMovementDirection()
                {

                    Vector3 _vDir = transform.forward * m_smoothInputVector.y;
                    Vector3 _hDir = transform.right * m_smoothInputVector.x;

                    Vector3 _desiredDir = _vDir + _hDir;
                    Vector3 _flattenDir = FlattenVectorOnSlopes(_desiredDir);

                    m_finalMoveDir = _flattenDir;
                }

                protected virtual Vector3 FlattenVectorOnSlopes(Vector3 _vectorToFlat)
                {
                    if(m_isGrounded)
                        _vectorToFlat = Vector3.ProjectOnPlane(_vectorToFlat,m_hitInfo.normal);
                    
                    return _vectorToFlat;
                }

                protected virtual void CalculateSpeed()
                {
                    m_currentSpeed = m_isRunning && CanRun() ? runSpeed : walkSpeed;
                    m_currentSpeed = m_isCrouching ? crouchSpeed : m_currentSpeed;
                    m_currentSpeed = m_isSliding ? slideSpeed : m_currentSpeed;
                    m_currentSpeed = InputManager._MovementAxis == Vector2.zero ? 0f : m_currentSpeed;
                    m_currentSpeed = InputManager._MovementAxis.y == -1 ? m_currentSpeed * moveBackwardsSpeedPercent : m_currentSpeed;
                    m_currentSpeed = InputManager._MovementAxis.x != 0 && InputManager._MovementAxis.y ==  0 ? m_currentSpeed * moveSideSpeedPercent :  m_currentSpeed;
                }

                protected virtual void CalculateFinalMovement()
                {
                    float _smoothInputVectorMagnitude = experimental ? m_smoothInputVectorMagnitude : 1f;
                    Vector3 _finalVector = m_smoothFinalMoveDir * m_finalSmoothCurrentSpeed * _smoothInputVectorMagnitude;

                    // We have to assign individually in order to make our character jump properly because before it was overwriting Y value and that's why it was jerky now we are adding to Y value and it's working
                    m_finalMoveVector.x = _finalVector.x ;
                    m_finalMoveVector.z = _finalVector.z ;

                    if(m_characterController.isGrounded) // Thanks to this check we are not applying extra y velocity when in air so jump will be consistent
                        m_finalMoveVector.y += _finalVector.y ; //so this makes our player go in forward dir using slope normal but when jumping this is making it go higher so this is weird
                }
            #endregion

            #region Crouching Methods
                protected virtual void HandleCrouchInput()
                {
                    if(!CanCrouch()) return;

                    if(m_isRunning && !m_isCrouching && InputManager._MovementAxis != Vector2.zero && CanRun())
                        HandleSlide();
                    else
                        HandleCrouch();
                }

                protected virtual void HandleSlide()
                {
                    m_isSliding = true;
                    m_duringSlideAnimation = true;

                    m_headBob.CurrentStateHeight = m_slideCamHeight;
                    
                    DOTween.To(() => m_characterController.height, x => m_characterController.height = x, m_slideHeight, slideTransitionDuration).SetEase(slideTransitionCurve);

                    DOTween.To(() => m_characterController.center, x => m_characterController.center = x, m_slideCenter, slideTransitionDuration).SetEase(slideTransitionCurve);

                    m_yawTransform.DOLocalMoveY(m_slideCamHeight,slideTransitionDuration).SetEase(slideTransitionCurve);

                    this._CallWithDelay(ReturnToInitHeight, maxSlideDuration);
                }

                protected virtual void ReturnToInitHeight() // TODO make sure you cannot return when there is roof above
                {
                    if(CheckIfRoof())
                    {
                        DOTween.Kill(this);
                        m_isSliding = false;
                        HandleCrouch();
                        return;
                    }

                    if(!m_isSliding) return;

                    DOTween.Kill(this);

                    m_isSliding = false;
                    m_duringSlideAnimation = false;

                    m_headBob.CurrentStateHeight = m_initCamHeight;
                    
                    DOTween.To(() => m_characterController.height, x => m_characterController.height = x, m_initHeight, slideTransitionDuration).SetEase(slideTransitionCurve);

                    DOTween.To(() => m_characterController.center, x => m_characterController.center = x, m_initCenter, slideTransitionDuration).SetEase(slideTransitionCurve);

                    m_yawTransform.DOLocalMoveY(m_initCamHeight,slideTransitionDuration).SetEase(slideTransitionCurve);
                }

                protected virtual void HandleCrouch()
                {
                    if(m_isCrouching)
                        if(CheckIfRoof())
                            return;

                    if(m_LandRoutine != null)
                        StopCoroutine(m_LandRoutine);

                    m_duringCrouchAnimation = true;

                    float _currentHeight = m_characterController.height;
                    Vector3 _currentCenter = m_characterController.center;

                    float _desiredHeight = m_isCrouching ? m_initHeight : m_crouchHeight;
                    Vector3 _desiredCenter = m_isCrouching ? m_initCenter : m_crouchCenter;

                    Vector3 _camPos = m_yawTransform.localPosition;
                    float _camCurrentHeight = _camPos.y;
                    float _camDesiredHeight = m_isCrouching ? m_initCamHeight : m_crouchCamHeight;

                    m_isCrouching = !m_isCrouching;
                    m_headBob.CurrentStateHeight = m_isCrouching ? m_crouchCamHeight : m_initCamHeight;

                    DOTween.To(() => m_characterController.height, x => m_characterController.height = x, _desiredHeight, crouchTransitionDuration).SetEase(crouchTransitionCurve);

                    DOTween.To(() => m_characterController.center, x => m_characterController.center = x, _desiredCenter, crouchTransitionDuration).SetEase(crouchTransitionCurve);

                    m_yawTransform.DOLocalMoveY(_camDesiredHeight,crouchTransitionDuration).SetEase(crouchTransitionCurve).OnComplete(delegate {m_duringCrouchAnimation = false;});
                }
            #endregion

            #region Landing Methods
                protected virtual void HandleLanding()
                {
                    if(!m_previouslyGrounded && m_isGrounded)
                    {
                        InvokeLandingRoutine();
                    }
                }

                protected virtual void InvokeLandingRoutine()
                {
                    if(m_LandRoutine != null)
                        StopCoroutine(m_LandRoutine);

                    m_LandRoutine = LandingRoutine();
                    StartCoroutine(m_LandRoutine);
                }

                protected virtual IEnumerator LandingRoutine()
                {
                    float _percent = 0f;
                    float _landAmount = 0f;

                    float _speed = 1f / landDuration;

                    Vector3 _localPos = m_yawTransform.localPosition;
                    float _initLandHeight = _localPos.y;

                    _landAmount = m_inAirTimer > landTimer ? highLandAmount : lowLandAmount;

                    while(_percent < 1f)
                    {
                        _percent += Time.deltaTime * _speed;
                        float _desiredY = landCurve.Evaluate(_percent) * _landAmount;

                        _localPos.y = _initLandHeight + _desiredY;
                        m_yawTransform.localPosition = _localPos;

                        yield return null;
                    }
                }
            #endregion

            #region Locomotion Apply Methods

                protected virtual void HandleHeadBob()
                {
                    
                    if(InputManager._MovementAxis != Vector2.zero && m_isGrounded  && !m_hitWall)
                    {
                        if(!m_duringCrouchAnimation && !m_isSliding) // we want to make our head bob only if we are moving and not during crouch routine
                        {
                            m_headBob.ScrollHeadBob(m_isRunning && CanRun(),m_isCrouching, InputManager._MovementAxis);
                            m_yawTransform.localPosition = Vector3.Lerp(m_yawTransform.localPosition,(Vector3.up * m_headBob.CurrentStateHeight) + m_headBob.FinalOffset,Time.deltaTime * smoothHeadBobSpeed);
                        }
                    }
                    else // if we are not moving or we are not grounded
                    {
                        if(!m_headBob.Resetted)
                            m_headBob.ResetHeadBob();

                        if(!m_duringCrouchAnimation) // we want to reset our head bob only if we are standing still and not during crouch routine
                            m_yawTransform.localPosition = Vector3.Lerp(m_yawTransform.localPosition,new Vector3(0f,m_headBob.CurrentStateHeight,0f),Time.deltaTime * smoothHeadBobSpeed);
                    }

                    //m_camTransform.localPosition = Vector3.Lerp(m_camTransform.localPosition,m_headBob.FinalOffset,Time.deltaTime * smoothHeadBobSpeed);
                }

                protected virtual void HandleCameraSway()
                {
                    m_cameraController.HandleSway(m_smoothInputVector,InputManager._MovementAxis.x);
                }

                protected virtual void HandleRunFOV()
                {
                    if(!m_duringRunAnimation && InputManager._MovementAxis != Vector2.zero && !m_hitWall)
                    {
                        if(m_isRunning && CanRun())
                        {
                            m_duringRunAnimation = true;
                            m_cameraController.ChangeRunFOV(false);
                        }
                    }

                    if(m_duringRunAnimation)
                    {
                        if(InputManager._MovementAxis == Vector2.zero || !CanRun() || m_hitWall)
                        {
                            m_duringRunAnimation = false;
                            m_cameraController.ChangeRunFOV(true);
                        }
                    }
                }

                protected virtual void ChangeToRunFOV()
                {
                   if(!CanRun() || InputManager._MovementAxis == Vector2.zero) return;

                   m_duringRunAnimation = true;
                   m_cameraController.ChangeRunFOV(false);
                }

                protected virtual void ChangeToInitFOV()
                {
                    if(!m_duringRunAnimation) return;

                    m_duringRunAnimation = false;
                    m_cameraController.ChangeRunFOV(true);
                }

                protected virtual void HandleRunAnimation()
                {
                    if(InputManager._MovementAxis == Vector2.zero || m_hitWall)
                    {
                        if(m_duringRunAnimation)
                        {
                            m_duringRunAnimation = false;
                            m_cameraController.ChangeRunFOV(true);
                        }
                    }
                }

                protected virtual void HandleJump()
                {
                    if(!CanJump()) return;

                    //m_finalMoveVector.y += jumpSpeed /* m_currentSpeed */; // we are adding because ex. when we are going on slope we want to keep Y value not overwriting it
                    m_finalMoveVector.y = jumpSpeed /* m_currentSpeed */; // turns out that when adding to Y it is too much and it doesn't feel correct because jumping on slope is much faster and higher;
                
                    m_previouslyGrounded = true;
                    m_isGrounded = false;
                }
                protected virtual void ApplyGravity()
                {
                    if(m_characterController.isGrounded) // if we would use our own m_isGrounded it would not work that good, this one is more precise
                    {
                        m_inAirTimer = 0f;
                        m_finalMoveVector.y = Mathf.Clamp(m_finalMoveVector.y -= stickToGroundForce * Time.deltaTime, -stickToGroundForce, jumpSpeed); // Additional force to make our character stick to ground more but it's not neccesarry
                    }
                    else
                    {
                        m_inAirTimer += Time.deltaTime;
                        m_finalMoveVector += Physics.gravity * gravityMultiplier * Time.deltaTime;
                    }
                }

                protected virtual void ApplyMovement()
                {
                    m_characterController.Move(m_finalMoveVector * Time.deltaTime);
                }

                protected virtual void RotateTowardsCamera()
                {
                    Quaternion _currentRot = transform.rotation;
                    Quaternion _desiredRot = m_yawTransform.rotation;

                    transform.rotation = Quaternion.Slerp(_currentRot,_desiredRot,Time.deltaTime * smoothRotateSpeed);
                }
            #endregion
        #endregion
    }
}
