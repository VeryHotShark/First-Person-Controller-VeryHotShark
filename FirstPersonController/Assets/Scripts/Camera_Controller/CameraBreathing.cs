using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    public class CameraBreathing : MonoBehaviour
    {
        #region Variables
            [Space,Header("Data")]
            [SerializeField] private PerlinNoiseData data = null;

            [Space,Header("Axis")]
            [SerializeField] private bool x = true;
            [SerializeField] private bool y = true;
            [SerializeField] private bool z = true;

            private PerlinNoiseScroller m_perlinNoiseScroller;
            private Vector3 m_finalRot;
            private Vector3 m_finalPos;
        #endregion

        #region BuiltIn Methods

            void Start()
            {
                m_perlinNoiseScroller = new PerlinNoiseScroller(data);
            }

            void LateUpdate()
            {
                if(data != null)
                {
                    m_perlinNoiseScroller.UpdateNoise();

                    Vector3 _posOffset = Vector3.zero;
                    Vector3 _rotOffset = Vector3.zero;

                    switch (data.transformTarget)
                    {
                        case TransformTarget.Position:
                        {
                            if(x)
                                _posOffset.x += m_perlinNoiseScroller.Noise.x;

                            if(y)
                                _posOffset.y += m_perlinNoiseScroller.Noise.y;

                            if(z)
                                _posOffset.z += m_perlinNoiseScroller.Noise.z;

                            m_finalPos.x = x ? _posOffset.x : transform.localPosition.x;
                            m_finalPos.y = y ? _posOffset.y : transform.localPosition.y;
                            m_finalPos.z = z ? _posOffset.z : transform.localPosition.z;

                            transform.localPosition = m_finalPos;
                            break;
                        }
                        case TransformTarget.Rotation:
                        {
                            if(x)
                                _rotOffset.x += m_perlinNoiseScroller.Noise.x;

                            if(y)
                                _rotOffset.y += m_perlinNoiseScroller.Noise.y;

                            if(z)
                                _rotOffset.z += m_perlinNoiseScroller.Noise.z;

                            m_finalRot.x = x ? _rotOffset.x : transform.localEulerAngles.x;
                            m_finalRot.y = y ? _rotOffset.y : transform.localEulerAngles.y;
                            m_finalRot.z = z ? _rotOffset.z : transform.localEulerAngles.z;
                        
                            transform.localEulerAngles = m_finalRot;

                            break;
                        }
                        case TransformTarget.Both:
                        {
                            if(x)
                            {
                                _posOffset.x += m_perlinNoiseScroller.Noise.x;
                                _rotOffset.x += m_perlinNoiseScroller.Noise.x;
                            }

                            if(y)
                            {
                                _posOffset.y += m_perlinNoiseScroller.Noise.y;
                                _rotOffset.y += m_perlinNoiseScroller.Noise.y;
                            }

                            if(z)
                            {
                                _posOffset.z += m_perlinNoiseScroller.Noise.z;
                                _rotOffset.z += m_perlinNoiseScroller.Noise.z;
                            }

                            m_finalPos.x = x ? _posOffset.x : transform.localPosition.x;
                            m_finalPos.y = y ? _posOffset.y : transform.localPosition.y;
                            m_finalPos.z = z ? _posOffset.z : transform.localPosition.z;

                            m_finalRot.x = x ? _rotOffset.x : transform.localEulerAngles.x;
                            m_finalRot.y = y ? _rotOffset.y : transform.localEulerAngles.y;
                            m_finalRot.z = z ? _rotOffset.z : transform.localEulerAngles.z;

                            transform.localPosition = m_finalPos;
                            transform.localEulerAngles = m_finalRot;

                            break;
                        }
                    }

                    // We don't do that here because if we only use rotational perlin noise than it would override position of our camera to be Vector3.zero every frame and this would interfere with our Head Bobbing System
                    // transform.localPosition = _posOffset;
                    // transform.localEulerAngles = _rotOffset;
                }

                
            }   
        #endregion

    }
}
