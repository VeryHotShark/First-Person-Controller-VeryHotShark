using System.Collections;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{
    [RequireComponent(typeof(CharacterController))]
    public class FirstPersonController : MonoBehaviour
    {
        #region Variables
            #region Public      
                #region Data
                    [Space]
                    [BoxGroup("Data")] public MovementInputData movementInputData;
                    [BoxGroup("Data")] public HeadBobData headBobData;

                #endregion
                    
                #region Locomotion
                    [Space]
                    [BoxGroup("Locomotion Settings")] public float crouchSpeed = 1f;
                    [BoxGroup("Locomotion Settings")] public float walkSpeed = 2f;
                    [BoxGroup("Locomotion Settings")] public float runSpeed = 3f;
                    [BoxGroup("Locomotion Settings")] public float jumpSpeed = 5f;
                    [BoxGroup("Locomotion Settings")][Slider(0f,1f)] public float moveBackwardsSpeedPercent = 0.5f;
                    [BoxGroup("Locomotion Settings")][Slider(0f,1f)]  public float moveSideSpeedPercent = 0.75f;
                #endregion

                #region Run Settings
                    [Space]
                    [BoxGroup("Run Settings")][Slider(-1f,1f)] public float canRunThreshold = 0.8f;
                    [BoxGroup("Run Settings")] public AnimationCurve runTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Crouch Settings
                    [Space]
                    [BoxGroup("Crouch Settings")] [Slider(0.2f,0.9f)] public float crouchPercent = 0.6f;
                    [BoxGroup("Crouch Settings")] public float crouchTransitionDuration = 1f;
                    [BoxGroup("Crouch Settings")] public AnimationCurve crouchTransitionCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Landing Settings
                    [Space]
                    [BoxGroup("Landing Settings")] [Slider(0.05f,0.5f)] public float lowLandAmount = 0.1f;
                    [BoxGroup("Landing Settings")] [Slider(0.2f,0.9f)] public float highLandAmount = 0.6f;
                    [BoxGroup("Landing Settings")] public float landTimer = 0.5f;
                    [BoxGroup("Landing Settings")] public float landDuration = 1f;
                    [BoxGroup("Landing Settings")] public AnimationCurve landCurve = AnimationCurve.EaseInOut(0f,0f,1f,1f);
                #endregion

                #region Gravity
                    [Space]
                    [BoxGroup("Gravity Settings")] public float gravityMultiplier = 2.5f;
                    [BoxGroup("Gravity Settings")] public float stickToGroundForce = 5f;
                    [Space]
                    [BoxGroup("Gravity Settings")] public LayerMask groundLayer;
                    [BoxGroup("Gravity Settings")] [Slider(0f,1f)] public float rayLength = 0.1f;
                    [BoxGroup("Gravity Settings")] [Slider(0.01f,1f)] public float raySphereRadius = 0.1f;
                #endregion

                #region Wall Settings
                    [BoxGroup("Gravity Settings")] public LayerMask obstacleLayers;
                    [BoxGroup("Check Wall Settings")] [Slider(0f,1f)] public float rayObstacleLength = 0.1f;
                    [BoxGroup("Check Wall Settings")] [Slider(0.01f,1f)] public float rayObstacleSphereRadius = 0.1f;
                    
                #endregion

                #region Smooth Settings
                    [Space]                
                    [BoxGroup("Smooth Settings")] [Range(1f,100f)] public float smoothRotateSpeed = 5f;
                    [BoxGroup("Smooth Settings")] [Range(1f,100f)] public float smoothInputSpeed = 5f;
                    [BoxGroup("Smooth Settings")] [Range(1f,100f)] public float smoothVelocitySpeed = 5f;
                    [BoxGroup("Smooth Settings")] [Range(1f,100f)] public float smoothFinalDirectionSpeed = 5f;
                    [BoxGroup("Smooth Settings")] [Range(1f,100f)] public float smoothHeadBobSpeed = 5f;

                    [Space]
                    [BoxGroup("Smooth Settings")] public bool experimental;
                    [InfoBox("It should smooth our player movement to not start fast and not stop fast but it's somehow jerky", InfoBoxType.Warning)]
                    [Tooltip("If set to very high it will stop player immediately after releasing input, otherwise it just another smoothing to our movement to make our player not move fast immediately and not stop immediately")]
                    [BoxGroup("Smooth Settings")] [ShowIf("experimental")] [Range(1f,100f)] public float smoothInputMagnitudeSpeed = 5f;
                    
                #endregion
            #endregion
            #region Private
                #region Components / Custom Classes
                    CharacterController m_characterController;
                    Transform m_yawTransform;
                    Transform m_camTransform;
                    HeadBob m_headBob;
                    CameraController m_cameraController;
                #endregion

                #region Debug/Cache
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector2 m_inputVector;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector2 m_smoothInputVector;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector3 m_finalMoveDir;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector3 m_smoothFinalMoveDir;
                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector3 m_finalMoveVector;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_currentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_smoothCurrentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_finalSmoothCurrentSpeed;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_walkRunSpeedDifference;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_finalRayLength;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] bool m_hitWall;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] bool m_isGrounded;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] bool m_previouslyGrounded;

                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_initHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_crouchHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector3 m_initCenter;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] Vector3 m_crouchCenter;
                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_initCamHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_crouchCamHeight;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_crouchStandHeightDifference;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] bool m_duringCrouchAnimation;
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] bool m_duringRunAnimation;
                    [Space]
                    [BoxGroup("DEBUG")][SerializeField][ReadOnly] float m_inAirTimer;

                    [Space]
                    [BoxGroup("DEBUG")][ShowIf("experimental")][SerializeField][ReadOnly] float m_inputVectorMagnitude;
                    [BoxGroup("DEBUG")][ShowIf("experimental")][SerializeField][ReadOnly] float m_smoothInputVectorMagnitude;
                #endregion
            
                #region Cache Non Serialized
                    RaycastHit m_hitInfo;
                    IEnumerator m_CrouchRoutine;
                    IEnumerator m_LandRoutine;
                #endregion
            #endregion
        
        #endregion

        #region BuiltIn Methods     
            void Start()
            {
                GetComponents();
                InitVariables();
            }

            void Update()
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

                    // Handle Player Movement, Gravity, Jump, Crouch etc.
                    HandleCrouch();
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

        #region Custom Methods
            #region Initialize Methods    
                void GetComponents()
                {
                    m_characterController = GetComponent<CharacterController>();
                    m_cameraController = GetComponentInChildren<CameraController>();
                    m_yawTransform = m_cameraController.transform;
                    m_camTransform = GetComponentInChildren<Camera>().transform;
                    m_headBob = new HeadBob(headBobData, moveBackwardsSpeedPercent, moveSideSpeedPercent);
                }

                void InitVariables()
                {   
                    // Calculate where our character center should be based on height and skin width
                    m_characterController.center = new Vector3(0f,m_characterController.height / 2f + m_characterController.skinWidth,0f);

                    m_initCenter = m_characterController.center;
                    m_initHeight = m_characterController.height;

                    m_crouchHeight = m_initHeight * crouchPercent;
                    m_crouchCenter = (m_crouchHeight / 2f + m_characterController.skinWidth) * Vector3.up;

                    m_crouchStandHeightDifference = m_initHeight - m_crouchHeight;

                    m_initCamHeight = m_yawTransform.localPosition.y;
                    m_crouchCamHeight = m_initCamHeight - m_crouchStandHeightDifference;

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
                void SmoothInput()
                {
                    m_inputVector = movementInputData.InputVector.normalized;
                    m_smoothInputVector = Vector2.Lerp(m_smoothInputVector,m_inputVector,Time.deltaTime * smoothInputSpeed);
                    //Debug.DrawRay(transform.position, new Vector3(m_smoothInputVector.x,0f,m_smoothInputVector.y), Color.green);
                }

                void SmoothSpeed()
                {
                    m_smoothCurrentSpeed = Mathf.Lerp(m_smoothCurrentSpeed, m_currentSpeed, Time.deltaTime * smoothVelocitySpeed);

                    if(movementInputData.IsRunning && CanRun())
                    {
                        float _walkRunPercent = Mathf.InverseLerp(walkSpeed,runSpeed, m_smoothCurrentSpeed);
                        m_finalSmoothCurrentSpeed = runTransitionCurve.Evaluate(_walkRunPercent) * m_walkRunSpeedDifference + walkSpeed;
                    }
                    else
                    {
                        m_finalSmoothCurrentSpeed = m_smoothCurrentSpeed;
                    }
                }

                void SmoothDir()
                {

                    m_smoothFinalMoveDir = Vector3.Lerp(m_smoothFinalMoveDir, m_finalMoveDir, Time.deltaTime * smoothFinalDirectionSpeed);
                    Debug.DrawRay(transform.position, m_smoothFinalMoveDir, Color.yellow);
                }
                
                void SmoothInputMagnitude()
                {
                    m_inputVectorMagnitude = m_inputVector.magnitude;
                    m_smoothInputVectorMagnitude = Mathf.Lerp(m_smoothInputVectorMagnitude, m_inputVectorMagnitude, Time.deltaTime * smoothInputMagnitudeSpeed);
                }
            #endregion

            #region Locomotion Calculation Methods
                void CheckIfGrounded()
                {
                    Vector3 _origin = transform.position + m_characterController.center;

                    bool _hitGround = Physics.SphereCast(_origin,raySphereRadius,Vector3.down,out m_hitInfo,m_finalRayLength,groundLayer);
                    Debug.DrawRay(_origin,Vector3.down * (m_finalRayLength),Color.red);

                    m_isGrounded = _hitGround ? true : false;
                }

                void CheckIfWall()
                {
                    
                    Vector3 _origin = transform.position + m_characterController.center;
                    RaycastHit _wallInfo;

                    bool _hitWall = false;

                    if(movementInputData.HasInput && m_finalMoveDir.sqrMagnitude > 0)
                        _hitWall = Physics.SphereCast(_origin,rayObstacleSphereRadius,m_finalMoveDir, out _wallInfo,rayObstacleLength,obstacleLayers);
                    Debug.DrawRay(_origin,m_finalMoveDir * rayObstacleLength,Color.blue);

                    m_hitWall = _hitWall ? true : false;
                }

                bool CheckIfRoof() /// TO FIX
                {
                    Vector3 _origin = transform.position;
                    RaycastHit _roofInfo;

                    bool _hitRoof = Physics.SphereCast(_origin,raySphereRadius,Vector3.up,out _roofInfo,m_initHeight);

                    return _hitRoof;
                }

                bool CanRun()
                {
                    Vector3 _normalizedDir = Vector3.zero;

                    if(m_smoothFinalMoveDir != Vector3.zero)
                        _normalizedDir = m_smoothFinalMoveDir.normalized;

                    float _dot = Vector3.Dot(transform.forward,_normalizedDir);
                    return _dot >= canRunThreshold && !movementInputData.IsCrouching ? true : false;
                }

                void CalculateMovementDirection()
                {

                    Vector3 _vDir = transform.forward * m_smoothInputVector.y;
                    Vector3 _hDir = transform.right * m_smoothInputVector.x;

                    Vector3 _desiredDir = _vDir + _hDir;
                    Vector3 _flattenDir = FlattenVectorOnSlopes(_desiredDir);

                    m_finalMoveDir = _flattenDir;
                }

                Vector3 FlattenVectorOnSlopes(Vector3 _vectorToFlat)
                {
                    if(m_isGrounded)
                        _vectorToFlat = Vector3.ProjectOnPlane(_vectorToFlat,m_hitInfo.normal);
                    
                    return _vectorToFlat;
                }

                void CalculateSpeed()
                {
                    m_currentSpeed = movementInputData.IsRunning && CanRun() ? runSpeed : walkSpeed;
                    m_currentSpeed = movementInputData.IsCrouching ? crouchSpeed : m_currentSpeed;
                    m_currentSpeed = !movementInputData.HasInput ? 0f : m_currentSpeed;
                    m_currentSpeed = movementInputData.InputVector.y == -1 ? m_currentSpeed * moveBackwardsSpeedPercent : m_currentSpeed;
                    m_currentSpeed = movementInputData.InputVector.x != 0 && movementInputData.InputVector.y ==  0 ? m_currentSpeed * moveSideSpeedPercent :  m_currentSpeed;
                }

                void CalculateFinalMovement()
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
                void HandleCrouch()
                {
                    if(movementInputData.CrouchClicked && m_isGrounded)
                        InvokeCrouchRoutine();
                }

                void InvokeCrouchRoutine()
                {
                    if(movementInputData.IsCrouching)
                        if(CheckIfRoof())
                            return;

                    if(m_LandRoutine != null)
                        StopCoroutine(m_LandRoutine);

                    if(m_CrouchRoutine != null)
                        StopCoroutine(m_CrouchRoutine);

                    m_CrouchRoutine = CrouchRoutine();
                    StartCoroutine(m_CrouchRoutine);
                }

                IEnumerator CrouchRoutine()
                {
                    m_duringCrouchAnimation = true;

                    float _percent = 0f;
                    float _smoothPercent = 0f;
                    float _speed = 1f / crouchTransitionDuration;

                    float _currentHeight = m_characterController.height;
                    Vector3 _currentCenter = m_characterController.center;

                    float _desiredHeight = movementInputData.IsCrouching ? m_initHeight : m_crouchHeight;
                    Vector3 _desiredCenter = movementInputData.IsCrouching ? m_initCenter : m_crouchCenter;

                    Vector3 _camPos = m_yawTransform.localPosition;
                    float _camCurrentHeight = _camPos.y;
                    float _camDesiredHeight = movementInputData.IsCrouching ? m_initCamHeight : m_crouchCamHeight;

                    movementInputData.IsCrouching = !movementInputData.IsCrouching;
                    m_headBob.CurrentStateHeight = movementInputData.IsCrouching ? m_crouchCamHeight : m_initCamHeight;
                    Debug.Log(m_headBob.CurrentStateHeight);

                    while(_percent < 1f)
                    {
                        _percent += Time.deltaTime * _speed;
                        _smoothPercent = crouchTransitionCurve.Evaluate(_percent);

                        m_characterController.height = Mathf.Lerp(_currentHeight,_desiredHeight,_smoothPercent);
                        m_characterController.center = Vector3.Lerp(_currentCenter,_desiredCenter,_smoothPercent);

                        _camPos.y = Mathf.Lerp(_camCurrentHeight,_camDesiredHeight, _smoothPercent);
                        m_yawTransform.localPosition = _camPos;

                        yield return null;
                    }

                    m_duringCrouchAnimation = false;
                }
                
            #endregion

            #region Landing Methods
                void HandleLanding()
                {
                    if(!m_previouslyGrounded && m_isGrounded)
                    {
                        InvokeLandingRoutine();
                    }
                }

                void InvokeLandingRoutine()
                {
                    if(m_LandRoutine != null)
                        StopCoroutine(m_LandRoutine);

                    m_LandRoutine = LandingRoutine();
                    StartCoroutine(m_LandRoutine);
                }

                IEnumerator LandingRoutine()
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

                void HandleHeadBob()
                {
                    
                    if(movementInputData.HasInput && m_isGrounded  && !m_hitWall)
                    {
                        if(!m_duringCrouchAnimation) // we want to make our head bob only if we are moving and not during crouch routine
                        {
                            m_headBob.ScrollHeadBob(movementInputData.IsRunning && CanRun(),movementInputData.IsCrouching, movementInputData.InputVector);
                            m_yawTransform.localPosition = Vector3.Lerp(m_yawTransform.localPosition,(Vector3.up * m_headBob.CurrentStateHeight) + m_headBob.FinalOffset,Time.deltaTime * smoothHeadBobSpeed);
                        }
                    }
                    else // if we are not moving or we are not grounded
                    {
                        if(!m_headBob.Resetted)
                        {
                            m_headBob.ResetHeadBob();
                        }

                        if(!m_duringCrouchAnimation) // we want to reset our head bob only if we are standing still and not during crouch routine
                            m_yawTransform.localPosition = Vector3.Lerp(m_yawTransform.localPosition,new Vector3(0f,m_headBob.CurrentStateHeight,0f),Time.deltaTime * smoothHeadBobSpeed);
                    }

                    //m_camTransform.localPosition = Vector3.Lerp(m_camTransform.localPosition,m_headBob.FinalOffset,Time.deltaTime * smoothHeadBobSpeed);
                }

                void HandleCameraSway()
                {
                    m_cameraController.HandleSway(m_smoothInputVector,movementInputData.InputVector.x);
                }

                void HandleRunFOV()
                {
                    if(movementInputData.HasInput && m_isGrounded  && !m_hitWall)
                    {
                        if(movementInputData.RunClicked && CanRun())
                        {
                            m_duringRunAnimation = true;
                            m_cameraController.ChangeRunFOV(false);
                        }

                        if(movementInputData.IsRunning && CanRun() && !m_duringRunAnimation )
                        {
                            m_duringRunAnimation = true;
                            m_cameraController.ChangeRunFOV(false);
                        }
                    }

                    if(movementInputData.RunReleased || !movementInputData.HasInput || m_hitWall)
                    {
                        if(m_duringRunAnimation)
                        {
                            m_duringRunAnimation = false;
                            m_cameraController.ChangeRunFOV(true);
                        }
                    }
                }
                void HandleJump()
                {
                    if(movementInputData.JumpClicked && !movementInputData.IsCrouching)
                    {
                        //m_finalMoveVector.y += jumpSpeed /* m_currentSpeed */; // we are adding because ex. when we are going on slope we want to keep Y value not overwriting it
                        m_finalMoveVector.y = jumpSpeed /* m_currentSpeed */; // turns out that when adding to Y it is too much and it doesn't feel correct because jumping on slope is much faster and higher;
                    
                        m_previouslyGrounded = true;
                        m_isGrounded = false;
                    }
                }
                void ApplyGravity()
                {
                    if(m_characterController.isGrounded) // if we would use our own m_isGrounded it would not work that good, this one is more precise
                    {
                        m_inAirTimer = 0f;
                        m_finalMoveVector.y = -stickToGroundForce;

                        HandleJump();
                    }
                    else
                    {
                        m_inAirTimer += Time.deltaTime;
                        m_finalMoveVector += Physics.gravity * gravityMultiplier * Time.deltaTime;
                    }
                }

                void ApplyMovement()
                {
                    m_characterController.Move(m_finalMoveVector * Time.deltaTime);
                }

                void RotateTowardsCamera()
                {
                    Quaternion _currentRot = transform.rotation;
                    Quaternion _desiredRot = m_yawTransform.rotation;

                    transform.rotation = Quaternion.Slerp(_currentRot,_desiredRot,Time.deltaTime * smoothRotateSpeed);
                }
            #endregion
        #endregion
    }
}
