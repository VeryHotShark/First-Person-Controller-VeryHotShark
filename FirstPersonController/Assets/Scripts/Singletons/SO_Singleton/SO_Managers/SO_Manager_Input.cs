using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VHS
{
    [CreateAssetMenu(fileName = "SO_InputManager", menuName = "SO_Managers/SO_InputManager")]
    public class SO_Manager_Input : SO_Singleton<SO_Manager_Input>
    {
        [SerializeField] private float m_doubleTapThreshold = 0.2f;

        public float DoubleTapThreshold => m_doubleTapThreshold;
    }
}

