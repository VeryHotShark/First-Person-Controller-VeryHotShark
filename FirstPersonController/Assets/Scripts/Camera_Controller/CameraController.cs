using NaughtyAttributes;
using UnityEngine;

namespace VHS
{    
    public class CameraController : MonoBehaviour
    {
        #region Variables
            #region Data
                [BoxGroup("Input Data")] public CameraInputData camInputData;

                [BoxGroup("Custom Classes")] public CamerZoom cameraZoom;
                [BoxGroup("Custom Classes")] public CameraSwaying cameraSway;
            #endregion

            #region Settings
                [Space]
                [BoxGroup("Settings")] public Vector2 sensitivity;
                [BoxGroup("Settings")] public Vector2 smoothAmount;
                [BoxGroup("Settings")] [MinMaxSlider(-90f,90f)] public Vector2 lookAngleMinMax;
            #endregion

            #region Private
                protected float m_yaw;
                protected float m_pitch;

                protected float m_desiredYaw;
                protected float m_desiredPitch;

                #region Components                    
                    protected Transform m_pitchTranform;
                    protected Camera m_cam;
                #endregion
            #endregion
            
        #endregion

        #region BuiltIn Methods     
            protected virtual void Start()
            {
                GetComponents();
                InitComponents();
                ChangeCursorState();
            }

            protected virtual void LateUpdate()
            {
                CalculateRotation();
                SmoothRotation();
                ApplyRotation();
                HandleZoom();
            }
        #endregion

        #region Custom Methods
            protected virtual void GetComponents()
            {
                m_pitchTranform = transform.GetChild(0).transform;
                m_cam = GetComponentInChildren<Camera>();
            }

            protected virtual void InitComponents()
            {
                cameraZoom.Init(m_cam, camInputData);
                cameraSway.Init(m_cam.transform);
            }

            protected virtual void CalculateRotation()
            {
                m_desiredYaw += camInputData.InputVector.x * sensitivity.x * Time.deltaTime;
                m_desiredPitch -= camInputData.InputVector.y * sensitivity.y * Time.deltaTime;

                m_desiredPitch = Mathf.Clamp(m_desiredPitch,lookAngleMinMax.x,lookAngleMinMax.y);
            }

            protected virtual void SmoothRotation()
            {
                m_yaw = Mathf.Lerp(m_yaw,m_desiredYaw, smoothAmount.x * Time.deltaTime);
                m_pitch = Mathf.Lerp(m_pitch,m_desiredPitch, smoothAmount.y * Time.deltaTime);
            }

            protected virtual void ApplyRotation()
            {
                transform.eulerAngles = new Vector3(0f,m_yaw,0f);
                m_pitchTranform.localEulerAngles = new Vector3(m_pitch,0f,0f);
            }

            protected virtual void HandleZoom()
            {
                if(camInputData.ZoomClicked || camInputData.ZoomReleased)
                    cameraZoom.ChangeFOV(this);
            }

            protected virtual void ChangeCursorState()
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }

            public virtual void HandleSway(Vector3 _inputVector,float _rawXInput)
            {
                cameraSway.SwayPlayer(_inputVector,_rawXInput);
            }


            public virtual void ChangeRunFOV(bool _returning)
            {
                cameraZoom.ChangeRunFOV(_returning,this);
            }

        #endregion
    }
}