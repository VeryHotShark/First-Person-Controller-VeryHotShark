using System.Collections;
using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    [System.Serializable]
    public class CamerZoom
    {
        #region Variables

            #region Public
                
                [Range(20f,60f)] public float zoomFOV;
                public AnimationCurve zoomCurve;
                public float zoomTransitionDuration;

                [Range(60f,100f)] public float runFOV;
                public AnimationCurve runCurve;
                public float runTransitionDuration;
                public float runReturnTransitionDuration;
            #endregion

            #region Private
                float m_initFOV;
                CameraInputData m_camInputData;
                MovementInputData m_movementInputData;

                #region Components
                    Camera m_cam;
                #endregion
                #region Reference/Cache
                    IEnumerator m_ChangeFOVRoutine;
                    IEnumerator m_ChangeRunFOVRoutine;
                #endregion
            #endregion
        #endregion
    
        #region Custom Methods
            public void Init(Camera _cam, CameraInputData _data)
            {
                m_camInputData = _data;

                m_cam = _cam;
                m_initFOV = m_cam.fieldOfView;
            }

            public void ChangeFOV(MonoBehaviour _mono)
            {
                if(m_ChangeFOVRoutine != null)
                    _mono.StopCoroutine(m_ChangeFOVRoutine);

                m_ChangeFOVRoutine = ChangeFOVRoutine();
                _mono.StartCoroutine(m_ChangeFOVRoutine);
            }

            IEnumerator ChangeFOVRoutine()
            {
                float _percent = 0f;
                float _smoothPercent = 0f;

                float _speed = 1f / zoomTransitionDuration;

                float _currentFOV = m_cam.fieldOfView;
                float  _targetFOV = m_camInputData.IsZooming ? m_initFOV : zoomFOV;

                m_camInputData.IsZooming = !m_camInputData.IsZooming;

                while(_percent < 1f)
                {
                    _percent += Time.deltaTime * _speed;
                    _smoothPercent = zoomCurve.Evaluate(_percent);
                    m_cam.fieldOfView = Mathf.Lerp(_currentFOV, _targetFOV, _smoothPercent);
                    yield return null;
                }
            }

            public void ChangeRunFOV(bool _returning,MonoBehaviour _mono)
            {
                if(m_ChangeRunFOVRoutine != null)
                    _mono.StopCoroutine(m_ChangeRunFOVRoutine);

                m_ChangeRunFOVRoutine = ChangeRunFOVRoutine(_returning);
                _mono.StartCoroutine(m_ChangeRunFOVRoutine);
            }

            IEnumerator ChangeRunFOVRoutine(bool _returning)
            {
                float _percent = 0f;
                float _smoothPercent = 0f;

                float _duration = _returning ? runReturnTransitionDuration : runTransitionDuration;
                float _speed = 1f / _duration;

                float _currentFOV = m_cam.fieldOfView;
                float  _targetFOV = _returning ? m_initFOV : runFOV;


                while(_percent < 1f)
                {
                    _percent += Time.deltaTime * _speed;
                    _smoothPercent = runCurve.Evaluate(_percent);
                    m_cam.fieldOfView = Mathf.Lerp(_currentFOV, _targetFOV, _smoothPercent);
                    yield return null;
                }
            }
        #endregion
    }
}