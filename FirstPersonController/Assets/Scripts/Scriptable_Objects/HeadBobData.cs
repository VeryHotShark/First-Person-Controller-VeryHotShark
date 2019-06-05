using UnityEngine;
using NaughtyAttributes;

namespace VHS
{
    [CreateAssetMenu(fileName = "HeadBobData", menuName = "FirstPersonController/Data/HeadBobData", order = 3)]
    public class HeadBobData : ScriptableObject
    {
        #region Variables    
            [BoxGroup("Curves")] public AnimationCurve xCurve;
            [BoxGroup("Curves")] public AnimationCurve yCurve;

            [Space]
            [BoxGroup("Amplitude")] public float xAmplitude;
            [BoxGroup("Amplitude")] public float yAmplitude;

            [Space]
            [BoxGroup("Frequency")] public float xFrequency;
            [BoxGroup("Frequency")] public float yFrequency;

            [Space]
            [BoxGroup("Run Multipliers")] public float runAmplitudeMultiplier;
            [BoxGroup("Run Multipliers")] public float runFrequencyMultiplier;

            [Space]
            [BoxGroup("Crouch Multipliers")] public float crouchAmplitudeMultiplier;
            [BoxGroup("Crouch Multipliers")] public float crouchFrequencyMultiplier;
        #endregion

        #region Properties
            public float MoveBackwardsFrequencyMultiplier {get;set;}
            public float MoveSideFrequencyMultiplier {get;set;}
        #endregion
    }
}
