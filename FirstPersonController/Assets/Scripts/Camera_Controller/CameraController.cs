using System;
using NaughtyAttributes;
using UnityEngine;

namespace VHS
{    
    public class CameraController : MonoBehaviour
    {
        #region Variables
            #region Data
                [Space,Header("Custom Classes")]
                [SerializeField] private CameraZoom cameraZoom = null;
                [SerializeField] private CameraSwaying cameraSway = null;

            #endregion

            #region Settings
                [Space,Header("Look Settings")]
                [SerializeField] private Vector2 sensitivity = Vector2.zero;
                [SerializeField] private Vector2 smoothAmount = Vector2.zero;
                [SerializeField] [MinMaxSlider(-90f,90f)] private Vector2 lookAngleMinMax = Vector2.zero;
            #endregion

            #region Private
               private float m_yaw;
               private float m_pitch;

               private float m_desiredYaw;
               private float m_desiredPitch;

                #region Components                    
                    private Transform m_pitchTranform;
                    private Camera m_cam;
                #endregion
            #endregion
            
        #endregion

        #region BuiltIn Methods  
            private void Awake()
            {
                GetComponents();
                InitValues();
                InitComponents();
                ChangeCursorState();
            }

            private void OnEnable()
            {
                InputManager._OnPlayerZoomPressed += _OnPlayerZoomPressed;
                InputManager._OnPlayerZoomReleased += _OnPlayerZoomReleased;
            }

            private void OnDisable()
            {
                InputManager._OnPlayerZoomPressed -= _OnPlayerZoomPressed;
                InputManager._OnPlayerZoomReleased -= _OnPlayerZoomReleased;
            }

            private void LateUpdate()
            {
                CalculateRotation();
                SmoothRotation();
                ApplyRotation();
            }
        #endregion

        #region Event Callback Methods    
            private void _OnPlayerZoomPressed()
            {
                cameraZoom.ChangeFOV(this);
            }

            private void _OnPlayerZoomReleased()
            {
                cameraZoom.ChangeFOV(this);
            }
        #endregion

        #region Custom Methods
            void GetComponents()
            {
                m_pitchTranform = transform.GetChild(0).transform;
                m_cam = GetComponentInChildren<Camera>();
            }

            void InitValues()
            {
                m_yaw = transform.eulerAngles.y;
                m_desiredYaw = m_yaw;
            }

            void InitComponents()
            {
                cameraZoom.Init(m_cam);
                cameraSway.Init(m_cam.transform);
            }

            void CalculateRotation()
            {
                m_desiredYaw += InputManager._LookAxis.x * sensitivity.x * Time.deltaTime;
                m_desiredPitch -= InputManager._LookAxis.y * sensitivity.y * Time.deltaTime;

                m_desiredPitch = Mathf.Clamp(m_desiredPitch,lookAngleMinMax.x,lookAngleMinMax.y);
            }

            void SmoothRotation()
            {
                m_yaw = Mathf.Lerp(m_yaw,m_desiredYaw, smoothAmount.x * Time.deltaTime);
                m_pitch = Mathf.Lerp(m_pitch,m_desiredPitch, smoothAmount.y * Time.deltaTime);
            }

            void ApplyRotation()
            {
                transform.eulerAngles = new Vector3(0f,m_yaw,0f);
                m_pitchTranform.localEulerAngles = new Vector3(m_pitch,0f,0f);
            }

            public void HandleSway(Vector3 _inputVector,float _rawXInput)
            {
                cameraSway.SwayPlayer(_inputVector,_rawXInput);
            }

            public void ChangeRunFOV(bool _returning)
            {
                cameraZoom.ChangeRunFOV(_returning,this);
            }

            void ChangeCursorState()
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        #endregion
    }
}
