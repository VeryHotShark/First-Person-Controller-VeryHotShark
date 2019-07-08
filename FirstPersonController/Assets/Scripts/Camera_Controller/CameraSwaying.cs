using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    [System.Serializable]
    public class CameraSwaying
    {
        #region Variables
            [BoxGroup("Sway Settings")] public float swayAmount;
            [BoxGroup("Sway Settings")] public float swaySpeed;
            [BoxGroup("Sway Settings")] public float returnSpeed;
            [BoxGroup("Sway Settings")] public float changeDirectionMultiplier;

            [BoxGroup("Sway Settings")] public AnimationCurve swayCurve;

            #region Private
                protected Transform m_camTransform;
                protected float _scrollSpeed;

                protected float m_xAmountThisFrame;
                protected float m_xAmountPreviousFrame;

                protected bool m_diffrentDirection;
            #endregion
        #endregion

        #region Custom Methods
            public virtual void Init(Transform _cam)
            {
                m_camTransform = _cam;
            }

            public virtual void SwayPlayer(Vector3 _inputVector, float _rawXInput)
            {
                float _xAmount = _inputVector.x;

                m_xAmountThisFrame = _rawXInput;

                if(_rawXInput != 0f) // if we have some input
                {
                    if(m_xAmountThisFrame != m_xAmountPreviousFrame && m_xAmountPreviousFrame != 0) // if our previous dir is not equal to current one and the previous one was not idle
                        m_diffrentDirection = true;

                    // then we multiplier our scroll so when changing direction it will sway to the other direction faster
                    float _speedMultiplier = m_diffrentDirection ? changeDirectionMultiplier : 1f;
                    _scrollSpeed += (_xAmount * swaySpeed * Time.deltaTime * _speedMultiplier);
                }
                else // if we are not moving so there is no input
                {
                    if(m_xAmountThisFrame == m_xAmountPreviousFrame) // check if our previous dir equals current dir
                        m_diffrentDirection = false; // if yes we want to reset this bool so basically it can be used correctly once we move again

                    _scrollSpeed = Mathf.Lerp(_scrollSpeed,0f,Time.deltaTime * returnSpeed);
                }

                _scrollSpeed = Mathf.Clamp(_scrollSpeed,-1f,1f);

                float _swayFinalAmount;

                if(_scrollSpeed < 0f)
                    _swayFinalAmount = -swayCurve.Evaluate(_scrollSpeed) * -swayAmount;
                else
                    _swayFinalAmount = swayCurve.Evaluate(_scrollSpeed) * -swayAmount;
                
                Vector3 _swayVector;
                _swayVector.z = _swayFinalAmount;

                m_camTransform.localEulerAngles = new Vector3(m_camTransform.eulerAngles.x,m_camTransform.eulerAngles.y,_swayVector.z);

                m_xAmountPreviousFrame = m_xAmountThisFrame;
            }
        #endregion
    }
}