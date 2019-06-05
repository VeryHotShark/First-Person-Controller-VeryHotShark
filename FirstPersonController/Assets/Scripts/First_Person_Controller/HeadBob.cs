using UnityEngine;

namespace  VHS
{    
    public class HeadBob
    {
        #region Variables
            HeadBobData m_data;

            float m_xScroll;
            float m_yScroll;

            bool m_resetted;
            Vector3 m_finalOffset;
            float m_currentStateHeight = 0f;
        #endregion

        #region Properties
            public Vector3 FinalOffset => m_finalOffset;
            public bool Resetted => m_resetted;
            public float CurrentStateHeight
            {
                get => m_currentStateHeight;
                set => m_currentStateHeight = value;
            }
        #endregion

        #region Custom Methods
            public HeadBob(HeadBobData _data,float _moveBackwardsMultiplier,float _moveSideMultiplier)
            {
                m_data = _data;

                m_data.MoveBackwardsFrequencyMultiplier = _moveBackwardsMultiplier;
                m_data.MoveSideFrequencyMultiplier = _moveSideMultiplier;

                m_xScroll = 0f;
                m_yScroll = 0f;

                m_resetted = false;
                m_finalOffset = Vector3.zero;
            }

            public void ScrollHeadBob(bool _running, bool _crouching, Vector2 _input)
            {
                m_resetted = false;

                float _amplitudeMultiplier;
                float _frequencyMultiplier;
                float _additionalMultiplier; // when moving backwards or to sides

                _amplitudeMultiplier = _running ? m_data.runAmplitudeMultiplier : 1f;
                _amplitudeMultiplier = _crouching ? m_data.crouchAmplitudeMultiplier : _amplitudeMultiplier;

                _frequencyMultiplier = _running ? m_data.runFrequencyMultiplier : 1f;
                _frequencyMultiplier = _crouching ? m_data.crouchFrequencyMultiplier : _frequencyMultiplier;

                _additionalMultiplier = _input.y == -1 ? m_data.MoveBackwardsFrequencyMultiplier : 1f;
                _additionalMultiplier = _input.x != 0 & _input.y == 0 ? m_data.MoveSideFrequencyMultiplier : _additionalMultiplier;


                m_xScroll += Time.deltaTime * m_data.xFrequency * _frequencyMultiplier ; // you can also multiply this by _additionalMultiplier but it looks unnatural a bit;
                m_yScroll += Time.deltaTime * m_data.yFrequency * _frequencyMultiplier ;

                float _xValue;
                float _yValue;

                _xValue = m_data.xCurve.Evaluate(m_xScroll);
                _yValue = m_data.yCurve.Evaluate(m_yScroll);

                m_finalOffset.x = _xValue * m_data.xAmplitude * _amplitudeMultiplier * _additionalMultiplier;
                m_finalOffset.y = _yValue * m_data.yAmplitude * _amplitudeMultiplier * _additionalMultiplier;
            }

            public void ResetHeadBob()
            {
                m_resetted = true;

                m_xScroll = 0f;
                m_yScroll = 0f;

                m_finalOffset = Vector3.zero;
            }
        #endregion
    }
}
