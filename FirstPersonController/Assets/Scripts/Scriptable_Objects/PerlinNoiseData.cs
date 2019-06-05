using UnityEngine;

namespace VHS
{
    public enum TransformTarget
    {
        Position,
        Rotation,
        Both
    }

    [CreateAssetMenu(fileName = "PerlinNoiseData", menuName = "FirstPersonController/Data/PerlinNoiseData", order = 2)]
    public class PerlinNoiseData : ScriptableObject
    {
        #region Variables
            public TransformTarget transformTarget;

            [Space]
            public float amplitude;
            public float frequency;
        #endregion    
    }
}