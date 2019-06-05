using UnityEngine;

namespace VHS
{    
    public class PerlinNoiseScroller
    {
        #region Variables
            PerlinNoiseData m_data;

            Vector3 m_noiseOffset;
            Vector3 m_noise;
        #endregion

        #region Properties
            public Vector3 Noise => m_noise;
        #endregion

        #region Custom Methods
            public PerlinNoiseScroller (PerlinNoiseData _data)
            {
                m_data = _data;

                float _rand = 32f;

                m_noiseOffset.x = Random.Range(0f,_rand);
                m_noiseOffset.y = Random.Range(0f,_rand);
                m_noiseOffset.z = Random.Range(0f,_rand);
            }

            public void UpdateNoise()
            {
                float _scrollOffset = Time.deltaTime * m_data.frequency;

                m_noiseOffset.x += _scrollOffset;
                m_noiseOffset.y += _scrollOffset;
                m_noiseOffset.z += _scrollOffset;

                m_noise.x = Mathf.PerlinNoise(m_noiseOffset.x,0f);
                m_noise.y = Mathf.PerlinNoise(m_noiseOffset.x,1f);
                m_noise.z = Mathf.PerlinNoise(m_noiseOffset.x,2f);

                m_noise -= Vector3.one * 0.5f;
                m_noise *= m_data.amplitude;
            }
        #endregion
    }
}
